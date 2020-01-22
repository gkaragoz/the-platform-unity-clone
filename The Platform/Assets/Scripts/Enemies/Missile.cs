using UnityEngine;

public class Missile : MonoBehaviour, IPooledObject {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Enemy _enemy = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Transform _destinationTransform = null;

    private void Awake() {
        _enemy = GetComponent<Enemy>();
    }

    private void MoveToDestination() {
        LeanTween.move( this.gameObject, 
                        new Vector3(transform.position.x, 
                                    transform.position.y, 
                                    _destinationTransform.position.z),
                        _enemy.EnemyStats.GetMovementSpeed())
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(_enemy.OnCrashed);
    }

    public void SetDestinationTransform(Transform destination) {
        this._destinationTransform = destination;
    }

    public void OnObjectReused() {
        MoveToDestination();
        gameObject.SetActive(true);
    }
}
