using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotateAroundComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_target != null)
            _transform.RotateAround(_target.position, _direction, _speed * Time.deltaTime);
    }
}
