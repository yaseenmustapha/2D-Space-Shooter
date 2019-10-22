using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopSpawning = false;

    public void StartSpawning() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawning) {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawning) {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], spawnPos, Quaternion.identity);
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
