using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerHealth _health;
    private PlayerMovement _movement;
    private AnimationSystem _animationSystem;
    private PlayerPickUp _pickup;

    public void Init(PlayerSetting settings)
    {
        _pickup = Instantiate(settings.pickUpPrefab,transform);
        _animationSystem = gameObject.AddComponent<AnimationSystem>();
        _animationSystem.Init();
        _health = gameObject.AddComponent<PlayerHealth>();
        _health.Init(settings.beginhealth,settings.damageMaterial);
        _movement = gameObject.AddComponent<PlayerMovement>();
        _movement.Init(_animationSystem);
    }
}
