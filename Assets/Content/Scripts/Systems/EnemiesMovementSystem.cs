using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovementSystem : MonoBehaviour
{
    private EnemiesSpawnSystem _spawnSystem;
    private GameSettings _gameSettings;

    public void Init(EnemiesSpawnSystem spawnSystem, GameSettings gameSettings)
    {
        _spawnSystem = spawnSystem;
        _gameSettings = gameSettings;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            foreach (var item in _spawnSystem.GetAllAliveEnemies())
            {
                item.Move();
            }
            yield return new WaitForSeconds(_gameSettings.gameSetting.moveUpdateRate);
        }
    }
}
