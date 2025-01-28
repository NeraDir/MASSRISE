using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Action playerMove;

    private CharacterController _characterController;

    private Transform _transform;

    private AnimationSystem _animatorController;

    private Quaternion _lastRotation;

    private int _characterState = 0;
    private float _rotationVelocity = 0;

    private const string _animationKey = "characterStateIndex";

    private const float WalkSpeedModifier = 2.5f;
    private const float MoveSpeed = 5;
    private const float RotationSmoothTime = 0.04f;

    public void Init(AnimationSystem animation)
    {
        _transform = transform;
        _animatorController = animation;
        _characterController = gameObject.AddComponent<CharacterController>();
        _characterController.center = new Vector3(0, 1, 0);
        _characterController.radius = 0.2f;
        _characterController.height = 2f;
    }

    private void Update()
    {
        if (_characterController == null)
            return;

        Vector3 input = GetMovementDirection();
        bool isWalking = Input.GetKey(KeyCode.LeftControl);

        if (input.x != 0 || input.z != 0)
        {
            Move(input, isWalking);
            playerMove?.Invoke();
            Rotate(input);
        }
        else
        {
            _transform.rotation = _lastRotation;
            _characterState = 0;
        }
        _animatorController.SetAnimationState(_animationKey, _characterState);
    }

    private void Move(Vector3 input, bool walking)
    {
        Vector3 direction = new Vector3(input.x, 0, input.z);

        float currentSpeed = walking ? (MoveSpeed / WalkSpeedModifier) : MoveSpeed;
        _characterState = walking ? 1 : 2;
        _characterController.Move(direction.normalized * currentSpeed * Time.deltaTime);
    }

    private void Rotate(Vector3 input)
    {
        float _targetRotation = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;

        _transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(_transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime), 0);
        _lastRotation = _transform.rotation;
    }

    private Vector3 GetMovementDirection()
    {
        float x = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        float z = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        return new Vector3(x, 0, z).normalized;
    }
}
