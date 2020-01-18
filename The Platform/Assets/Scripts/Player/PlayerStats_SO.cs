using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Player";

    [SerializeField]
    [Utils.ReadOnly]
    private int _currentScore = 0;

    [SerializeField]
    [Utils.ReadOnly]
    private int _highScore = 0;

    [SerializeField]
    [Utils.ReadOnly]
    private int _gold = 0;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    public int CurrentScore {
        get { return _currentScore; }
        set { _currentScore = value; }
    }
    public int HighScore {
        get { return _highScore; }
        set { _highScore = value; }
    }
    public int Gold {
        get { return _gold; }
        set { _gold = value; }
    }

    #endregion

}