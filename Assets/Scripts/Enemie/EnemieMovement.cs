using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Transform _target;

    private AnimationController _controller;

    private float _speed = 2.5f;

    private const string _animationKey = "characterStateIndex";

    public void Init(Transform target)
    {
        _controller = GetComponentInChildren<AnimationController>();
        _controller.Init();
        _target = target;
        _agent = gameObject.AddComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _agent.acceleration = 360;
        _agent.angularSpeed = 360;
    }

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            OnMove();
        }
    }

    private void OnMove()
    {
        if (_agent == null)
            return;
        _controller.SetAnimationState(_animationKey, 1);
        _agent.SetDestination(_target.position);
    }
}