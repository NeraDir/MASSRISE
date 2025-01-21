using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemieHealth : MonoBehaviour
{
    public Action<float> changeHealth;

    private HealthBar _healthBar;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private float _barSpeed = 10;
    private Transform _transform;
    private Vector3 _barOffset = new Vector3(0, 2.48f, 0.58f);

    private float _currentHealth;
    private float _maxHealth;

    private Material _damageColor;
    private Material _defaultSkinnedMaterial;
    private List<Material> _defaultMeshesMaterial = new List<Material>();

    private Coroutine _waitingDamage;
    private Coroutine _waitAndChangeColor;

    private MeshRenderer[] _meshRenderers;

    private GameObject _destroyEffect;
    private GameObject _dropItem;

    [Inject]
    public void Init(float currentHealth, float maxHealth, Material damage, HealthBar healthBar)
    {
        _damageColor = damage;
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _defaultSkinnedMaterial = _skinnedMeshRenderer.material;
        foreach (var item in _meshRenderers)
        {
            _defaultMeshesMaterial.Add(item.material);
        }
        _transform = transform;
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
        _healthBar = healthBar;
        _destroyEffect =Resources.Load<GameObject>("Effects/deadEffect");
        _dropItem = Resources.Load<GameObject>("GamePrefabs/Coin");
        if (_healthBar != null)
        {
            _healthBar.Init();
            _healthBar.SetFollow(_transform, _barOffset, _barSpeed); 
            _healthBar.Hide();
            changeHealth += OnTakeDamage;
        }
    }

    private void OnEnable()
    {
        if(_healthBar != null)
        {
            _currentHealth = _maxHealth;
            _healthBar.gameObject.SetActive(true);
            _healthBar.Hide();
            changeHealth += OnTakeDamage;
        }
    }

    private void OnDisable()
    {
        changeHealth -= OnTakeDamage;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnTakeDamage(1);
        }
    }

    private void OnTakeDamage(float damage)
    {
        if (_currentHealth <= 0)
        {
            
            return;
        }
            
        if (_waitingDamage != null)
            StopCoroutine(_waitingDamage);
        if (_waitAndChangeColor != null)
            StopCoroutine(_waitAndChangeColor);

        _skinnedMeshRenderer.material = _damageColor;
        foreach (var item in _meshRenderers)
        {
            item.material = _damageColor;
        }

        _currentHealth -= damage;
        if (_healthBar != null)
        {
            _healthBar.Show();
            _healthBar.ChangeValue(_currentHealth, _maxHealth);
        }
        if (_currentHealth <= 0)
        {
            OnDead();
        }
        _waitAndChangeColor = StartCoroutine(WaitAndChangeColor());
        _waitingDamage = StartCoroutine(WaitDamage());
    }

    private void OnDead()
    {
        _transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            Instantiate(_destroyEffect, _transform.position, Quaternion.identity);
            Instantiate(_dropItem, new Vector3(_transform.position.x, _transform.position.y + 1, _transform.position.z), Quaternion.identity);
            _healthBar.Hide();
            _healthBar.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }

    private IEnumerator WaitAndChangeColor()
    {
        yield return new WaitForSeconds(0.15f);
        _skinnedMeshRenderer.material = _defaultSkinnedMaterial;
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = _defaultMeshesMaterial[i];
        }
    }

    private IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(1);
        _healthBar.Hide();
    }
}
