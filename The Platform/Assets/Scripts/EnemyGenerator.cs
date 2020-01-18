using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    [System.Serializable]
    public class Settings {
        public float rockSpawnRate = 1f;
        public float bladeSpawnRate = 5f;
    }

    [Header("Initializations")]
    [SerializeField]
    private Settings _settings = null;

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


    public IEnumerator ICheckRocks() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_settings.rockSpawnRate);

        while (true) {
            yield return waitForSeconds;

            SpawnRock();
        }
    }

    public IEnumerator ICheckBlades() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_settings.bladeSpawnRate);

        while (true) {
            yield return waitForSeconds;

            SpawnBlade();
        }
    }

    public void SpawnRock() {
        Vector3 randomPosition = new Vector3(
            0f, 
            GameManager.instance.RockFallPivotTransform.position.y, 
            Random.Range(GameManager.instance.LeftMapPivotTransform.position.z, GameManager.instance.RightMapPivotTransform.position.z));
        Quaternion randomQuaternion = Quaternion.Euler(0f, Random.Range(0, 180), 0f);
        ObjectPooler.instance.SpawnFromPool("Rock", randomPosition, randomQuaternion);
    }

    public void SpawnBlade() {
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

    public void StartGenerate() {
        _isRunning = true;

        _checkRocksCoroutine = StartCoroutine(ICheckRocks());
        _checkBladesCoroutine = StartCoroutine(ICheckBlades());
    }

    public void StopGenerate() {
        _isRunning = false;

        StopAllCoroutines();

        _checkRocksCoroutine = null;
        _checkBladesCoroutine = null;
    }

}
