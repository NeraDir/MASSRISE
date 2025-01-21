using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    public Action<float> changeHealth;

    private HealthBar _healthBar;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private float _barSpeed = 10;
    private Transform _transform;
    private Vector3 _barOffset = new Vector3(0,2.48f,0.58f);

    private float _currentHealth;
    private float _maxHealth;

    private Material _damageColor;
    private Material _defaultSkinnedMaterial;
    private List<Material> _defaultMeshesMaterial = new List<Material>();

    private Coroutine _waitingDamage;
    private Coroutine _waitAndChangeColor;

    private MeshRenderer[] _meshRenderers;

    [Inject]
    public void Init(float currentHealth,float maxHealth, Material damage)
    {
        Application.targetFrameRate = 90;
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
        _healthBar = Instantiate(Resources.Load<HealthBar>("GamePrefabs/HealthBar"));
        _healthBar.Init();
        _healthBar.SetFollow(_transform, _barOffset, _barSpeed);
        changeHealth += OnTakeDamage;
        StartCoroutine(WaitDamage());
    }

    private void OnDestroy()
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

        _waitAndChangeColor = StartCoroutine(WaitAndChangeColor());
        _waitingDamage = StartCoroutine(WaitDamage());
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
