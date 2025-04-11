using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Transform _target;

    private float _speed = 2.5f;

    public void Init(Transform target)
    {
        _target = target;
        _agent = gameObject.AddComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _agent.acceleration = 360;
        _agent.angularSpeed = 360;
    }

    public void OnMove()
    {
        if (_agent == null)
            return;
        _agent.SetDestination(_target.position);
    }
}
