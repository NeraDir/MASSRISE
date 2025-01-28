using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform _target;
    private Transform _transform;

    private Rigidbody _body;

    private bool _isPickUped = false;

    private const float YIncerement = 4f;
    private const float DelayTime = 0.25f;

    private void Start()
    {
        _transform = transform;
        _body = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform target)
    {
        if (_isPickUped)
            return;
        _isPickUped = true;
        _target = target;
        OnPickUp();
    }

    private void OnPickUp()
    {
        Destroy(_body);
        Vector3 currentPosition = _transform.position;


        _transform.DOMoveY(currentPosition.y + YIncerement, DelayTime).OnComplete(() =>
        {
            _transform.DOScale(Vector3.zero, DelayTime);
            _transform.DOMove(_target.position, DelayTime).OnComplete(() =>
            {
                OnPickUpCompleted();
            });
        });
    }

    public virtual void OnPickUpCompleted()
    {
        Destroy(gameObject);
    }
}
