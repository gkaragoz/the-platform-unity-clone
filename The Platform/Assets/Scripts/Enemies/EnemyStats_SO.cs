using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Scriptable Objects/Enemy Stats")]
public class EnemyStats_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Enemy";

    [SerializeField]
    private float _spawnRate = 0;

    [SerializeField]
    private int _crashScore = 0;

    [SerializeField]
    private float _hideTime = 0f;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    public float SpawnRate {
        get { return _spawnRate; }
        set { _spawnRate = value; }
    }
    public int CrashScore {
        get { return _crashScore; }
        set { _crashScore = value; }
    }
    public float HideTime { 
        get { return _hideTime; }
        set { _hideTime = value; }
    }

    #endregion

}