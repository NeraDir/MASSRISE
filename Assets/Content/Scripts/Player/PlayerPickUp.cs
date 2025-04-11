using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private Transform _transform;
    private SphereCollider _sphereCollider;
    private float _radius = 2.25f;

    public void Init()
    {
        _sphereCollider = gameObject.AddComponent<SphereCollider>();
        _transform = transform;
        _sphereCollider.radius = _radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.PickUp(_transform);
        }
    }
}
