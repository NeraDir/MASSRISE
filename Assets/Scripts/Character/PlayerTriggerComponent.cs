using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerComponent : MonoBehaviour
{
    private AnimationController _controller;
    private List<RockTriggerComponent> _activeTriggers = new List<RockTriggerComponent>();
    private RockTriggerComponent _currentTrigger;

    public void Init(AnimationController animation)
    {
        _controller = animation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RockTriggerComponent rockTrigger))
        {
            if (!_activeTriggers.Contains(rockTrigger))
                _activeTriggers.Add(rockTrigger);

            UpdateCurrentTrigger();

            if (_currentTrigger != null && _currentTrigger.GetTriggerState() == RockTriggerState.Near)
            {
                _controller.TakePickAxe();
            }
            if (_currentTrigger != null && _currentTrigger.GetTriggerState() == RockTriggerState.Mining)
            {
                _controller.Mining();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out RockTriggerComponent rockTrigger))
        {
            _activeTriggers.Remove(rockTrigger);

            UpdateCurrentTrigger();

            if (_currentTrigger != null)
            {
                if (_currentTrigger.GetTriggerState() == RockTriggerState.Near)
                {
                    _controller.TakePickAxe();
                    _controller.StopMining();
                }
            }
            else
            {
                _controller.StopMining();
                _controller.HidePickAxe();
            }
        }
    }

    public void OnRockDestroyed(RockTriggerComponent rockTrigger)
    {
        if (_activeTriggers.Contains(rockTrigger))
        {
            _activeTriggers.Remove(rockTrigger);

            UpdateCurrentTrigger();

            if (_currentTrigger == null)
            {
                _controller.StopMining();
                _controller.HidePickAxe();
            }
            else if (_currentTrigger.GetTriggerState() == RockTriggerState.Near)
            {
                _controller.TakePickAxe();
                _controller.StopMining();
            }
        }
    }

    private void UpdateCurrentTrigger()
    {
        float minDistance = float.MaxValue;
        RockTriggerComponent nearestTrigger = null;

        foreach (var trigger in _activeTriggers)
        {
            if (trigger == null) continue;

            float distance = Vector3.Distance(transform.position, trigger.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTrigger = trigger;
            }
        }

        _currentTrigger = nearestTrigger;
    }
}
