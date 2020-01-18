﻿using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public bool IsRunning { get { return _isRunning; } }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isRunning = false;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkRocksCoroutine = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkBladesCoroutine = null;


    private IEnumerator ICheckRocks() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyDB.instance.GetSpawnRate("Rock"));

        while (true) {
            yield return waitForSeconds;

            SpawnRock();
        }
    }

    private IEnumerator ICheckBlades() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyDB.instance.GetCrashScore("Rock"));

        while (true) {
            yield return waitForSeconds;

            SpawnBlade();
        }
    }

    private void SpawnRock() {
        Vector3 randomPosition = new Vector3(
            0f, 
            GameManager.instance.RockFallPivotTransform.position.y, 
            Random.Range(GameManager.instance.LeftMapPivotTransform.position.z, GameManager.instance.RightMapPivotTransform.position.z));
        Quaternion randomQuaternion = Quaternion.Euler(0f, Random.Range(0, 180), 0f);
        ObjectPooler.instance.SpawnFromPool("Rock", randomPosition, randomQuaternion);
    }

    private void SpawnBlade() {
        float zPos = 0f;
        if (Random.Range(0, 2) == 0) {
            zPos = GameManager.instance.LeftMapPivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                0f,
                -0.6f,
                zPos);

            ObjectPooler.instance.SpawnFromPool("BladeForward", spawnPosition, Quaternion.identity);
        } else {
            zPos = GameManager.instance.RightMapPivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                0f,
                -0.6f,
                zPos);

            ObjectPooler.instance.SpawnFromPool("BladeBackward", spawnPosition, Quaternion.identity);
        }
    }

    public void StartGenerateAll() {
        _isRunning = true;

        StartRockSpawns();
        StartBladeSpawns();
    }

    public void StopGenerateAll() {
        _isRunning = false;

        StopAllCoroutines();
    }

    public void StartRockSpawns() {
        if (_checkRocksCoroutine == null) {
            _checkRocksCoroutine = StartCoroutine(ICheckRocks());
            _isRunning = true;
        }
    }

    public void StopRockSpawns() {
        StopCoroutine(_checkRocksCoroutine);
        _isRunning = CheckAllCoroutineRunStatus();
    }

    public void StartBladeSpawns() {
        if (_checkBladesCoroutine == null) {
            _checkBladesCoroutine = StartCoroutine(ICheckBlades());
            _isRunning = true;
        }
    }

    public void StopBladeSpawn() {
        StopCoroutine(_checkBladesCoroutine);
        _isRunning = CheckAllCoroutineRunStatus();
    }

    private bool CheckAllCoroutineRunStatus() {
        if (_checkRocksCoroutine == null && _checkBladesCoroutine == null) {
            return false;
        } else {
            return true;
        }
    }

}
