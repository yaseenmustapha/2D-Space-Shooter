﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update() {
        
    }

    IEnumerator SpawnRoutine() {
        while (!_stopSpawning) {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
