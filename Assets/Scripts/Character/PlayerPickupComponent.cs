using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupComponent : MonoBehaviour
{
    private Transform _transform;
    private SphereCollider _sphereCollider;
    private Transform _target;
    private float _radius = 2.25f;

    public void Init(SphereCollider sphereCollider, Transform target)
    {
        _transform = transform;
        _target = target;
        _sphereCollider = sphereCollider;
        _sphereCollider.radius = _radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.PickUp(_target);
        }
    }
}
