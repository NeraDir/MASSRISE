using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectFollowComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_transform != null)
        {
            if (_target != null)
                _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, _speed * Time.deltaTime);
        }
    }

    public void SetData(Transform target,Vector3 offset, float speed)
    {
        _target = target;
        _offset = offset;
        _speed = speed;
    }
}
