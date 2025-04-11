using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private Transform _parent;
    private void Awake()
    {
        _parent = transform.parent;

        Player newPlayer = Instantiate(_gameSettings.playerSetting.playerPrefab,_gameSettings.playerSetting.spawnPoint,Quaternion.identity);
        newPlayer.Init(_gameSettings.playerSetting);

        ObjectPooler newPoller = Instantiate(_gameSettings.gameSetting.objectPooler, _parent);

        EnemiesSpawnSystem spawnSystem = Instantiate(_gameSettings.gameSetting.enemySpawnSystem, _parent);
        spawnSystem.Init(_gameSettings,newPlayer.transform);

        EnemiesMovementSystem movement = Instantiate(_gameSettings.gameSetting.enemyMovementSystem, _parent);
        movement.Init(spawnSystem,_gameSettings);

        ObjectsFollowSystem followsSystem = Instantiate(_gameSettings.gameSetting.objectsFollowSystem, _parent);
        followsSystem.Init(_gameSettings);

        Sun newSun = Instantiate(_gameSettings.gameSetting.sun, _parent);
        newSun.Init();

        FollowerComponent newCamera = Instantiate(_gameSettings.gameSetting.camera).GetComponentInChildren<FollowerComponent>();
        newCamera.SetData(newPlayer.transform,_gameSettings.gameSetting.cameraOffset, _gameSettings.gameSetting.speed);
    }
}
