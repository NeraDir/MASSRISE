using System;
using UnityEngine;

[Serializable]
public class SpawnSettings
{
    public Transform Player;
    public GameObject EnemyPrefab;
    public float SpawnRadius = 10f;
    public float SafeRadius = 5f;
    public float SpawnInterval = 2f;
    public float EnemySpeed = 3f;
    public float EnemyMaxHealth = 100f;
}