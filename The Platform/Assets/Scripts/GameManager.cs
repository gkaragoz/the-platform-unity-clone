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
    private Transform _leftMapPivotTransform = null;
    [SerializeField]
    private Transform _rightMapPivotTransform = null;
    [SerializeField]
    private Transform _rockFallPivotTransform = null;

    public Transform LeftMapPivotTransform { get { return _leftMapPivotTransform; } }
    public Transform RightMapPivotTransform { get { return _rightMapPivotTransform; } }
    public Transform RockFallPivotTransform { get { return _rockFallPivotTransform; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("Rock");
        ObjectPooler.instance.InitializePool("BladeForward");
        ObjectPooler.instance.InitializePool("BladeBackward");

        InitializeBladeDestinations();
    }

    private void InitializeBladeDestinations() {
        foreach (GameObject bladeObject in ObjectPooler.instance.GetGameObjectsOnPool("BladeForward")) {
            bladeObject.GetComponent<Blade>().SetDestinationTransform(_rightMapPivotTransform);
        }
        foreach (GameObject bladeObject in ObjectPooler.instance.GetGameObjectsOnPool("BladeBackward")) {
            bladeObject.GetComponent<Blade>().SetDestinationTransform(_leftMapPivotTransform);
        }
    }

}
