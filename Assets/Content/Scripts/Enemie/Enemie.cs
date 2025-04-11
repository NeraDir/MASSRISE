using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    private EnemieHealth _health;
    private EnemieMovement _movement;

    private Transform _transform;
    private Vector3 _beginScale;

    public void Init(EnemiesSetting settings, Transform target)
    {
        _health = gameObject.AddComponent<EnemieHealth>();
        _health.Init(settings.beginHealth, settings.damageMaterial);
        _movement = gameObject.AddComponent<EnemieMovement>();
        _movement.Init(target);

        _transform = transform;
        _beginScale = _transform.localScale;
    }

    private void OnEnable()
    {
        _transform.DOScale(_beginScale, 0.15f);
    }

    public void Move()
    {
        _movement.OnMove();
    }
}
