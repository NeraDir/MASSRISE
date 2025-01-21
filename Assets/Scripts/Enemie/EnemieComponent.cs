using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieComponent : MonoBehaviour
{
    [SerializeField] private Material _damageMaterial;

    private EnemieMovement _movement;
    private EnemieHealth _health;
    private EnemieAttack _attack;
    private TorchIKController _torchIKController;
    private EnemieData _enemieData;

    private Transform _transform;

    private Vector3 _beginScale;

    private HealthBar _healthBar;

    private void OnEnable()
    {
        _transform.DOScale(_beginScale, 0.15f);
    }

    public void Init(Transform target = null, EnemieData data = default, HealthBar healthBar = null)
    {
        _transform = GetComponent<Transform>();
        _beginScale = _transform.localScale;
        _healthBar = healthBar;
        _enemieData = data;
        _movement = gameObject.AddComponent<EnemieMovement>();
        _torchIKController = GetComponentInChildren<TorchIKController>();
        _movement.Init(target);
        _torchIKController.Init();
        _health = gameObject.AddComponent<EnemieHealth>();
        _health.Init(1, 1, _damageMaterial, healthBar);
        _attack = gameObject.AddComponent<EnemieAttack>();
    }

    public HealthBar GetHealthBar()
    {
        return _healthBar;
    }
}

public struct EnemieData
{
    public float Health { get; set; }
    public int Damage { get; set; }
}