using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RockTriggerState
{
    Near,
    Mining,
}

public class RockTriggerComponent : MonoBehaviour
{
    [SerializeField] private RockTriggerState _state;

    public RockTriggerState GetTriggerState()
    {
        return _state;
    }
}
