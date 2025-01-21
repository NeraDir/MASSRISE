using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieViewerH : MonoBehaviour
{
    public void LinkEntity(EcsEntity entity)
    {
        // Привязка Unity-объекта к ECS-сущности
        Debug.Log($"Привязан ECS-сущность к объекту {gameObject.name}");
    }
}

