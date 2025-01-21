using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotateComponent : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_transform != null)
        {
            _transform.Rotate(_direction, _speed * Time.deltaTime);
        }
    }
}
