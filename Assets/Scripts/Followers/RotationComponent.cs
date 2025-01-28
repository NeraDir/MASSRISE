using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_transform != null)
            _transform.Rotate(new Vector3(0, 1, 0), 360 * Time.deltaTime);
    }
}
