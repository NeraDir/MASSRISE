using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieViewerH : MonoBehaviour
{
    public void LinkEntity(EcsEntity entity)
    {
        // �������� Unity-������� � ECS-��������
        Debug.Log($"�������� ECS-�������� � ������� {gameObject.name}");
    }
}

