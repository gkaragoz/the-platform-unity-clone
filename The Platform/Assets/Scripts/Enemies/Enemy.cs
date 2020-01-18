using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private EnemyStats _enemyStats = null;

    public EnemyStats EnemyStats { 
        get {
            return _enemyStats;
        }
    }

    private void Start() {
        _enemyStats = GetComponent<EnemyStats>();
    }

    public void OnCrashed() {
        Invoke("Hide", _enemyStats.GetHideTime());

        int crashScore = _enemyStats.GetCrashScore();
        GameManager.instance.AddScoreToPlayer(crashScore);

        Debug.Log(_enemyStats.name + " crashed and got " + crashScore);
    }

    private void Hide() {
        this.gameObject.SetActive(false);
    }

}
