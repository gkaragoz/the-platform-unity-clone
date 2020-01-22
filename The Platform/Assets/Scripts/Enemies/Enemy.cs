using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private EnemyStats _enemyStats = null;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _hasCrashed = false;

    public EnemyStats EnemyStats { 
        get {
            return _enemyStats;
        }
    }
    
    public bool HasCrashed {
        get {
            return _hasCrashed;
        }
    }

    private void Awake() {
        _enemyStats = GetComponent<EnemyStats>();
    }

    public void OnCrashed() {
        Invoke("Hide", _enemyStats.GetHideTime());

        int crashScore = _enemyStats.GetCrashScore();
        GameManager.instance.AddScoreToPlayer(crashScore);

        Debug.Log(_enemyStats.name + " crashed and got " + crashScore);

        _hasCrashed = true;
    }

    private void Hide() {
        this.gameObject.SetActive(false);
    }

}
