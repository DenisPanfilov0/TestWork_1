using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyTypePrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private Transform _spawnZone;

    private void Start()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 zoneMin = _spawnZone.position - _spawnZone.localScale / 2;
            Vector3 zoneMax = _spawnZone.position + _spawnZone.localScale / 2;
            float randomX = Random.Range(zoneMin.x, zoneMax.x);
            float randomY = Random.Range(zoneMin.y, zoneMax.y);
            GameObject randomEnemyPrefab = _enemyTypePrefab[Random.Range(0, _enemyTypePrefab.Count)];
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}