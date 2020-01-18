using UnityEngine;

public class EnemyStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private EnemyStats_SO _enemyDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
    private EnemyStats_SO _enemy = null;

    #region Initializations

    private void Awake() {
        if (_enemyDefinition_Template != null) {
            _enemy = Instantiate(_enemyDefinition_Template);
        }
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _enemyDefinition_Template.Name;
    }

    public float GetSpawnRate() {
        return _enemy.SpawnRate;
    }

    public int GetCrashScore() {
        return _enemy.CrashScore;
    }

    public float GetHideTime() {
        return _enemy.HideTime;
    }

    #endregion

}