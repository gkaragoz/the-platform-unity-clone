using UnityEngine;

public class EnemyDB : MonoBehaviour {

    #region Singleton

    public static EnemyDB instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Initializations")]
    [SerializeField]
    private EnemyStats_SO[] _enemies = null;

    public EnemyStats_SO GetEnemy(string name) {
        for (int ii = 0; ii < _enemies.Length; ii++) {
            if (_enemies[ii].Name == name) {
                return _enemies[ii];
            }
        }
        return null;
    }

    public float GetCrashScore(string name) {
        return GetEnemy(name).CrashScore;
    }

    public float GetSpawnRate(string name) {
        return GetEnemy(name).SpawnRate;
    }

}