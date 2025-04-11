using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnSystem : MonoBehaviour
{
    private Transform _player;
    private GameSettings _gameSettings;

    public List<Enemie> _enemiesPool = new List<Enemie>();

    public void Init(GameSettings settings, Transform player)
    {
        _gameSettings = settings;
        _player = player;
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (_enemiesPool.Count < _gameSettings.gameSetting.enemiesMaxCount)
        {
            yield return new WaitForSeconds(_gameSettings.gameSetting.spawnRate);
            _enemiesPool.Add(SpawnEnemy());
        }
        while (true)
        {
            yield return new WaitForSeconds(_gameSettings.gameSetting.spawnRate);
            OnRespawn();
        }
    }

    private void OnRespawn()
    {
        if (_enemiesPool.Count < _gameSettings.gameSetting.enemiesMaxCount)
            return;
        Enemie newEnemie = _enemiesPool.FindLast(x => !x.gameObject.activeInHierarchy);
        if (newEnemie != null)
        {
            newEnemie.gameObject.SetActive(true);
            newEnemie.transform.position = GetSpawnPosition();
        }
    }

    public List<Enemie> GetAllAliveEnemies()
    {
        List<Enemie> aliveEnemies = _enemiesPool.FindAll(x => x.gameObject.activeInHierarchy);

        return aliveEnemies;
    }

    private Enemie SpawnEnemy()
    {
        Enemie newEnemie = Instantiate(_gameSettings.gameSetting.enemiePrefabs[Random.Range(0, _gameSettings.gameSetting.enemiePrefabs.Length)], GetSpawnPosition(), Quaternion.identity);
        newEnemie.Init(_gameSettings.enemiesSetting, _player);
        newEnemie.transform.SetParent(transform);
        return newEnemie;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle.normalized * (_gameSettings.gameSetting.spawnRadius + _gameSettings.gameSetting.safeRadius);
        Vector3 spawnPosition = new Vector3(_player.position.x + randomPosition.x, _player.position.y, _player.position.z + randomPosition.y);
        return spawnPosition;
    }
}
