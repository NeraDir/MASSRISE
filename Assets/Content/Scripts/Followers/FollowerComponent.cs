using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerComponent : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;
    private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        ObjectsFollowSystem.onUpdateFollows.AddListener(OnMove);
    }

    private void OnDestroy()
    {
        ObjectsFollowSystem.onUpdateFollows.RemoveListener(OnMove);
    }

    private void OnMove()
    {
        if (_transform != null)
        {
            if (_target != null)
                _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, _speed * Time.deltaTime);
        }
    }

    public void SetData(Transform target, Vector3 offset, float speed)
    {
        _target = target;
        _offset = offset;
        _speed = speed;
    }
}
