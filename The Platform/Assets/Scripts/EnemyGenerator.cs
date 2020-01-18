using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _spawnRate = 0.5f; // Seconds
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning { get { return _isRunning; } }

    private float _nextSpawn;

    private void Update() {
        if (IsRunning && Time.time > _nextSpawn) {
            _nextSpawn = Time.time + _spawnRate;

            SpawnRock();
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
                0f,
                zPos);

            ObjectPooler.instance.SpawnFromPool("BladeForward", spawnPosition, Quaternion.identity);
        } else {
            zPos = GameManager.instance.RightMapPivotTransform.position.z;

            Vector3 spawnPosition = new Vector3(
                0f,
                0f,
                zPos);

            ObjectPooler.instance.SpawnFromPool("BladeBackward", spawnPosition, Quaternion.identity);
        }
    }

    public void StartGenerate() {
        _isRunning = true;
    }

    public void StopGenerate() {
        _isRunning = false;
    }

}
