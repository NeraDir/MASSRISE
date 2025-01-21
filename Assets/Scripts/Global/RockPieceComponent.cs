using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPieceComponent : MonoBehaviour
{
    [SerializeField] private RockComponent _rock;

    public void TakingDamage(int value)
    {
        _rock.TakeDamage?.Invoke(1);
    }
}
