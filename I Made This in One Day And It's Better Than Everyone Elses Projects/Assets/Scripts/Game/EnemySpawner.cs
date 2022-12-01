using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemySpawnPoints {
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
}

[Serializable]
public struct EnemyWave {
    public EnemySpawnPoints[] enemySpawnPoints;
    public int minEnemiesThreasold;
}

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private EnemyWave[] enemyWaves;

    int enemyWaveIndex = 0;

    Coroutine spawnEnemiesCoroutine;

    public void SpawnFirstWave() {
        SpawnEnemyWave();
        spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesLoop());
    }

    public void StopSpawning() {
        StopCoroutine(spawnEnemiesCoroutine);
    }

    public void DestroyAllEnemies() {
        EnemyPathfinding[] enemies = FindObjectsOfType<EnemyPathfinding>();
        for (int i = 0; i < enemies.Length; i++) {
            Destroy(enemies[i].gameObject);
        }
    }

    private IEnumerator SpawnEnemiesLoop() {
        while (GameController.instance.GameActive) {
            yield return new WaitForSeconds(5f);
            if (GetNumEnemies() < enemyWaves[enemyWaveIndex].minEnemiesThreasold) {
                SpawnEnemyWave();
            }
        }
    }

    private void SpawnEnemyWave() {
        SpawnEnemies(enemyWaves[Mathf.Clamp(enemyWaveIndex, 0, enemyWaves.Length - 1)]);

        enemyWaveIndex++;
    }

    private void SpawnEnemies(EnemyWave ew) {
        for (int i = 0; i < ew.enemySpawnPoints.Length; i++) {
            for (int x = 0; x < ew.enemySpawnPoints[i].spawnPoints.Length; x++) {
                if (ew.enemySpawnPoints[i].spawnPoints[x].childCount == 0) {
                    GameObject enemy = Instantiate(ew.enemySpawnPoints[i].enemyPrefab, ew.enemySpawnPoints[i].spawnPoints[x].position, Quaternion.identity);
                }
            }
        }
    }

    private int GetNumEnemies() {
        return FindObjectsOfType<EnemyPathfinding>().Length;
    }
}
