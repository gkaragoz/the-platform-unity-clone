using UnityEngine;

public class GameManager : MonoBehaviour {
    
    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private Transform _leftMapPivotTransform;
    [SerializeField]
    private Transform _rightMapPivotTransform;
    [SerializeField]
    private Transform _rockFallPivotTransform;

    public Transform LeftMapPivotTransform { get { return _leftMapPivotTransform; } }
    public Transform RightMapPivotTransform { get { return _rightMapPivotTransform; } }
    public Transform RockFallPivotTransform { get { return _rockFallPivotTransform; } }

}
