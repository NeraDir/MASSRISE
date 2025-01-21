using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemieSpawnManager : MonoBehaviour
{
    public static Action onRespawnEnemie;

    public HealthBar healthBar;
    public EnemieComponent enemyPrefab;
    public Transform player;
    public float spawnRadius = 10f;
    public int maxEnemiesCount = 300;
    public int maxHealthBarCount = 20;
    public float spawnInterval = 2f;
    public float safeRadius;

    public List<EnemieComponent> _enemiesPool = new List<EnemieComponent>();
    private List<HealthBar> _healthBarPool = new List<HealthBar>();
    private List<SkinnedMeshRenderer> _enemiesSkinsPool = new List<SkinnedMeshRenderer>();

    public void Awake()
    {
        StartCoroutine(Spawning());
        onRespawnEnemie += OnRespawn;
    }

    private void OnDestroy()
    {
        onRespawnEnemie -= OnRespawn;
    }

    private IEnumerator Spawning()
    {
        while (_enemiesPool.Count < maxEnemiesCount)
        {
            yield return new WaitForSeconds(spawnInterval);
            _enemiesPool.Add(SpawnEnemy());
        }
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            OnRespawn();
        }
    }

    private void OnRespawn()
    {
        if (_enemiesPool.Count < maxEnemiesCount)
            return;
        Vector2 randomPosition = Random.insideUnitCircle.normalized * (spawnRadius + safeRadius);
        Vector3 spawnPosition = new Vector3(player.position.x + randomPosition.x, player.position.y, player.position.z + randomPosition.y);
        EnemieComponent newEnemie = _enemiesPool.FindLast(x => !x.gameObject.activeInHierarchy);
        if (newEnemie != null)
        {
            newEnemie.GetHealthBar().transform.position = spawnPosition;
            newEnemie.gameObject.SetActive(true);
            newEnemie.transform.position = spawnPosition;
        }
    }

    private EnemieComponent SpawnEnemy()
    {
        Vector2 randomPosition = Random.insideUnitCircle.normalized * (spawnRadius + safeRadius);
        Vector3 spawnPosition = new Vector3(player.position.x + randomPosition.x, player.position.y, player.position.z + randomPosition.y);
        EnemieComponent newEnemie = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        HealthBar newHealth = null;
        newHealth = Instantiate(healthBar, newEnemie.transform.position, healthBar.transform.rotation);
        _healthBarPool.Add(newHealth);
        newEnemie.Init(player, healthBar: newHealth);
        _enemiesSkinsPool.Add(newEnemie.GetComponentInChildren<SkinnedMeshRenderer>());

        return newEnemie;
    }
}