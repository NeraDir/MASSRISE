using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public Action<float> changeHealth;

    private HealthBar _healthBar;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private Transform _transform;
    private GameObject _healthBarObject;

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

    public virtual void Init(float maxHealth = 0, Material damage = null)
    {
        _damageColor = damage;
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _defaultSkinnedMaterial = _skinnedMeshRenderer.sharedMaterial;
        foreach (var item in _meshRenderers)
        {
            _defaultMeshesMaterial.Add(item.material);
        }
        _transform = transform;
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        HealthBar healthBar = Resources.Load<HealthBar>("UI/HealthBar");
        _healthBar = Instantiate(healthBar, _transform.position, healthBar.transform.rotation);
        _healthBarObject = _healthBar.gameObject;
        _destroyEffect = Resources.Load<GameObject>("Effects/deadEffect");
        _dropItem = Resources.Load<GameObject>("Items/Coin");
        if (_healthBar != null)
        {
            _healthBar.Init();
            _healthBar.SetFollow(_transform);
            _healthBar.Hide();
            changeHealth += OnTakeDamage;
        }
    }

    private void OnEnable()
    {
        if (_healthBar != null)
        {
            _currentHealth = _maxHealth;
            _healthBarObject.SetActive(true);
            _healthBar.Hide();
            _healthBar.ChangeValue(_currentHealth, _maxHealth);
            changeHealth += OnTakeDamage;
            StartCoroutine(WaitAndEnable());
        }
    }

    private IEnumerator WaitAndEnable()
    {
        while (_transform.localScale.x * 0.8f < 0.8)
        {
            _healthBarObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        _healthBarObject.SetActive(true);
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

        _skinnedMeshRenderer.sharedMaterial = _damageColor;
        foreach (var item in _meshRenderers)
        {
            item.sharedMaterial = _damageColor;
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
            _healthBarObject.SetActive(false);
            gameObject.SetActive(false);
            OnDeadComplete();
        });
    }

    public virtual void OnDeadComplete()
    {

    }

    private IEnumerator WaitAndChangeColor()
    {
        yield return new WaitForSeconds(0.15f);
        _skinnedMeshRenderer.sharedMaterial = _defaultSkinnedMaterial;
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].sharedMaterial = _defaultMeshesMaterial[i];
        }
    }

    private IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(1);
        _healthBar.Hide();
    }
}