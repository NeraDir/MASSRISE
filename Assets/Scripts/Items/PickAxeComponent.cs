using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeComponent : MonoBehaviour
{
    [SerializeField] private GameObject _miningEffect;
    [SerializeField] private Transform _effectSpawnPosition;

    private RockPieceComponent _piece;

    private bool _isMining;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RockPieceComponent rock))
        {
            _piece = rock;
        }
    }

    public void Mine()
    {
        if (_piece == null)
            return;
        Instantiate(_miningEffect, _effectSpawnPosition.position, _miningEffect.transform.rotation);
        _piece.TakingDamage(1);
    }

    public void SetMiningState(bool value)
    {
        _isMining = value;
    }
}
