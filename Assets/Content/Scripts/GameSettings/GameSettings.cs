using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Game Settings", fileName = "New Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Game Settings")]
    public GameSetting gameSetting;

    [Space(16)]
    [Header("Enemie Settings")]
    public EnemiesSetting enemiesSetting;

    [Space(16)]
    [Header("Player Settings")]
    public PlayerSetting playerSetting;
}

[Serializable]
public struct GameSetting
{
    [Header("Enemies Spawn Settings")]
    public Enemie[] enemiePrefabs;
    public float spawnRadius;
    public float safeRadius;
    public float spawnRate;
    public int enemiesMaxCount;

    [Header("Systems Update Settings")]
    public float moveUpdateRate;
    public float followUpdateRate;

    [Header("Systems And Components")]
    public EnemiesSpawnSystem enemySpawnSystem;
    public EnemiesMovementSystem enemyMovementSystem;
    public ObjectsFollowSystem objectsFollowSystem;
    public ObjectPooler objectPooler;
    public GameObject camera;
    public Sun sun;
    public UserInput userInput;

    [Header("Camera Settings")]
    public Vector3 cameraOffset;
    public float speed;
}

[Serializable]
public struct PlayerSetting
{
    public Player playerPrefab;
    public PlayerPickUp pickUpPrefab;
    public float beginhealth;
    public Vector3 spawnPoint;
    public Material damageMaterial;
}

[Serializable]
public struct EnemiesSetting
{
    public float beginHealth;
    public float damage;
    public Material damageMaterial;
}