using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private GameObject _rockPrefab;
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
        }
    }

    public void SpawnRock() {
        Vector3 randomPosition = new Vector3(
            0f, 
            GameManager.instance.RockFallPivotTransform.position.y, 
            Random.Range(GameManager.instance.LeftMapPivotTransform.position.z, GameManager.instance.RightMapPivotTransform.position.z));
        Quaternion randomQuaternion = Quaternion.Euler(0f, Random.Range(0, 180), 0f);
        GameObject go = Instantiate(_rockPrefab, randomPosition, randomQuaternion);
        go.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void StartGenerate() {
        _isRunning = true;
    }

    public void StopGenerate() {
        _isRunning = false;
    }

}
