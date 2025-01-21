using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private Transform _moveToTarget;

    [SerializeField] private Material _damage;

    private PlayerMovementComponent _movement;
    private PlayerAttack _attack;
    private PlayerHealth _health;
    private AnimationController _animationController;
    private PlayerPickupComponent _pickUpComponent;
    private PlayerTriggerComponent _triggerComponent;

    private void Awake()
    {
        AddAnimationController();
        AddPickUpComponentn();
        AddTriggerComponent();
        _movement = gameObject.AddComponent<PlayerMovementComponent>();
        _attack = gameObject.AddComponent<PlayerAttack>();
        _health = gameObject.AddComponent<PlayerHealth>();
        _movement.Init(_animationController);
        _health.Init(_maxHealth, _maxHealth,_damage);
        _attack.Init();
    }

    private void AddTriggerComponent()
    {
        CapsuleCollider capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            _triggerComponent = capsuleCollider.gameObject.AddComponent<PlayerTriggerComponent>();
            _triggerComponent.Init(_animationController);
        }
    }

    private void AddPickUpComponentn()
    {
        SphereCollider sphereCollider = GetComponentInChildren<SphereCollider>();
        _pickUpComponent = sphereCollider.gameObject.AddComponent<PlayerPickupComponent>();
        _pickUpComponent.Init(sphereCollider, _moveToTarget);
    }

    private void AddAnimationController()
    {
        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            _animationController = GetComponentInChildren<AnimationController>();
            if (_animationController == null)
            {
                _animationController = animator.gameObject.AddComponent<AnimationController>();
                _animationController.Init();
            }
            else
            {
                _animationController.Init();
            }
        }
    }
}
