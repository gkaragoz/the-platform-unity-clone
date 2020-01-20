using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IPooledObject {

    [Header("Initializations")]
    [SerializeField]
    private float _time = 4f;

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
        LeanTween.move(this.gameObject, new Vector3(transform.position.x, transform.position.y, _destinationTransform.position.z), _time).setEase(LeanTweenType.easeInOutQuad);
    }

    public void SetDestinationTransform(Transform destination) {
        this._destinationTransform = destination;
    }

    public void OnObjectReused() {
        MoveToDestination();
        gameObject.SetActive(true);
    }
}
