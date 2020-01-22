using System;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public bool IsRunning { get { return _isRunning; } }

    [Header("Settings")]
    [SerializeField]
    private bool _stopRocks = false;
    [SerializeField]
    private bool _stopBlades = false;
    [SerializeField]
    private bool _stopMissiles = false;
    [SerializeField]
    private bool _stopCannonballs = false;

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
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkMissilesCoroutine = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkCannonballsCoroutine = null;

    private void Update() {
        if (_stopRocks) {
            StopRockSpawns();
        }
        if (_stopBlades) {
            StopBladeSpawns();
        }
        if (_stopMissiles) {
            StopMissileSpawns();
        }
        if (_stopCannonballs) {
            StopCannonballSpawns();
        }
    }

    private IEnumerator ICheckRocks() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyDB.instance.GetSpawnRate("Rock"));

        while (true) {
            yield return waitForSeconds;

            SpawnRock();
        }
    }

    private IEnumerator ICheckMissiles() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyDB.instance.GetSpawnRate("Missile"));

        while (true) {
            yield return waitForSeconds;

            SpawnMissile();
        }
    }

    private IEnumerator ICheckBlades() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyDB.instance.GetSpawnRate("Rock"));

        while (true) {
            yield return waitForSeconds;

            SpawnBlade();
        }
    }
    private IEnumerator ICheckCannonballs() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyDB.instance.GetSpawnRate("Cannonball"));

        while (true) {
            yield return waitForSeconds;

            SpawnCannonball();
        }
    }

    private void SpawnRock() {
        Vector3 randomPosition = new Vector3(
            0f,
            GameManager.instance.RockFallPivotTransform.position.y,
            UnityEngine.Random.Range(GameManager.instance.LeftMapPivotTransform.position.z, GameManager.instance.RightMapPivotTransform.position.z));
        Quaternion randomQuaternion = Quaternion.Euler(0f, UnityEngine.Random.Range(0, 180), 0f);
        ObjectPooler.instance.SpawnFromPool("Rock", randomPosition, randomQuaternion);
    }

    private void SpawnBlade() {
        float zPos = 0f;
        if (UnityEngine.Random.Range(0, 2) == 0) {
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

    private void SpawnMissile() {
        float xPos = 0f;
        float yPos = 0f;
        float zPos = 0f;
        yPos = 0f;
        xPos = 0f;
        if (UnityEngine.Random.Range(0, 2) == 0) {
            zPos = GameManager.instance.LeftMissilePivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                xPos,
                yPos,
                zPos);

            ObjectPooler.instance.SpawnFromPool("MissileForward", spawnPosition, Quaternion.identity);
        } else {
            zPos = GameManager.instance.RightMissilePivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                xPos,
                yPos,
                zPos);

            ObjectPooler.instance.SpawnFromPool("MissileBackward", spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnCannonball() {
        float xPos = 0f;
        float yPos = 0f;
        float zPos = 0f;

        yPos = 0;
        xPos = 0;
        if (UnityEngine.Random.Range(0, 2) == 0) {
            yPos = GameManager.instance.LeftCannonballPivotTransform.position.y;
            zPos = GameManager.instance.LeftCannonballPivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                xPos,
                yPos,
                zPos);

            ObjectPooler.instance.SpawnFromPool("CannonballForward", spawnPosition, Quaternion.identity);
        } else {
            yPos = GameManager.instance.RightCannonballPivotTransform.position.y;
            zPos = GameManager.instance.RightCannonballPivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                xPos,
                yPos,
                zPos);

            ObjectPooler.instance.SpawnFromPool("CannonballBackward", spawnPosition, Quaternion.identity);
        }
    }

    public void StartGenerateAll() {
        _isRunning = true;

        StartRockSpawns();
        StartBladeSpawns();
        StartMissileSpawns();
        StartCannonballSpawns();
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
        if (_checkMissilesCoroutine != null) {
            StopCoroutine(_checkRocksCoroutine);
        }

        _isRunning = CheckAllCoroutineRunStatus();
    }

    public void StartMissileSpawns() {
        if (_checkMissilesCoroutine == null) {
            _checkMissilesCoroutine = StartCoroutine(ICheckMissiles());
            _isRunning = true;
        }
    }

    public void StopMissileSpawns() {
        if (_checkMissilesCoroutine != null) {
            StopCoroutine(_checkMissilesCoroutine);
        }

        _isRunning = CheckAllCoroutineRunStatus();
    }

    public void StartBladeSpawns() {
        if (_checkBladesCoroutine == null) {
            _checkBladesCoroutine = StartCoroutine(ICheckBlades());
            _isRunning = true;
        }
    }

    public void StopBladeSpawns() {
        if (_checkMissilesCoroutine != null) {
            StopCoroutine(_checkBladesCoroutine);
        }

        _isRunning = CheckAllCoroutineRunStatus();
    }

    public void StartCannonballSpawns() {
        if (_checkCannonballsCoroutine == null) {
            _checkCannonballsCoroutine = StartCoroutine(ICheckCannonballs());
            _isRunning = true;
        }
    }

    public void StopCannonballSpawns() {
        if (_checkMissilesCoroutine != null) {
            StopCoroutine(_checkCannonballsCoroutine);
        }

        _isRunning = CheckAllCoroutineRunStatus();
    }

    private bool CheckAllCoroutineRunStatus() {
        if (_checkRocksCoroutine == null && _checkBladesCoroutine == null && _checkMissilesCoroutine == null && _checkCannonballsCoroutine == null) {
            return false;
        } else {
            return true;
        }
    }

}
