using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockComponent : MonoBehaviour
{
    public int MaxHeal;

    public Action<int> TakeDamage;

    [SerializeField] private GameObject[] _rockDestructionStates;
    [SerializeField] private GameObject _stoneChangeStateEffect;

    private Transform _transform;

    public List<int> _healPieces = new List<int>();

    private int _currentTypeIndex = 0;
    public int _heal;
    private void Awake()
    {
        _heal = MaxHeal;
        _transform = transform;
        TakeDamage += OnTakeDamage;
        SetStoneStates();
    }

    private void OnDestroy()
    {
        TakeDamage -= OnTakeDamage;
    }

    private void SetStoneStates()
    {
        int healPiece = _heal / _rockDestructionStates.Length;
        for (int i = 0; i < _rockDestructionStates.Length; i++)
        {
            _healPieces.Add(_heal - (i * healPiece));
        }
    }

    public void OnTakeDamage(int damage)
    {
        _heal -= damage;
        _heal = Mathf.Clamp(_heal, 0, MaxHeal);
        if (_heal <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            ChangeRockState();
        }
    }

    private void ChangeRockState()
    {
        for (int i = 0; i < _healPieces.Count; i++)
        {
            if (_heal > _healPieces[i])
            {
                SetStoneType(i);
                break;
            }
        }
    }

    private void SetStoneType(int typeIndex)
    {
        if (typeIndex != _currentTypeIndex)
        {
            _rockDestructionStates[_currentTypeIndex].SetActive(false);
            Instantiate(_stoneChangeStateEffect, _transform.position, Quaternion.identity);
            _rockDestructionStates[typeIndex].SetActive(true);
            _currentTypeIndex = typeIndex;
        }
    }
}
