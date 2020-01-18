using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour, IPooledObject {

    [Header("Initializations")]
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private Rigidbody _rb = null;
    [SerializeField]
    private float _movementSpeed = 100f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isMoving = false;
    [Utils.ReadOnly]
    [SerializeField]
    private Transform _destinationTransform = null;

    private const string PLACEMENT_ANIMATION_TRIGGER = "PLACEMENT_ANIMATION";
    private const string DISAPPEAR_ANIMATION_TRIGGER = "DISAPPEAR_ANIMATION";
    private bool _hasPlayedDisappearAnimation = false;

    private void Update() {
        if (_isMoving && HasDestination() && !HasReachedToTarget()) {
            Vector3 myPosition = transform.position;
            Vector3 destinationPosition = new Vector3(myPosition.x, myPosition.y, _destinationTransform.position.z);

            _rb.MovePosition(Vector3.MoveTowards(myPosition, destinationPosition, _movementSpeed * Time.deltaTime));
        } else if (HasDestination() && HasReachedToTarget()) {
            StopMovement();
            StartDisappearAnimation();
        }
    }

    public void StartMovement() {
        _isMoving = true;
    }

    public void StopMovement() {
        _isMoving = false;
    }

    public void SetDestinationTransform(Transform destination) {
        this._destinationTransform = destination;
    }

    public bool HasReachedToTarget() {
        return Mathf.Abs(transform.position.z - _destinationTransform.position.z) <= 0.001f ? true : false;
    }

    public bool HasDestination() {
        return (_destinationTransform == null) ? false : true;
    }

    public void OnObjectReused() {
        Debug.Log("OnObjectReused");
        _hasPlayedDisappearAnimation = false;

        StartPlacementAnimation();
        StartMovement();

        this.gameObject.SetActive(true);
    }

    public void SetActiveFalse() {
        this.gameObject.SetActive(false);
    }

    private void StartPlacementAnimation() {
        Debug.Log("StartPlacementAnimation");
        _animator.SetTrigger(PLACEMENT_ANIMATION_TRIGGER);
    }

    private void StartDisappearAnimation() {
        if (_hasPlayedDisappearAnimation == false) {
            Debug.Log("StartDisappearAnimation");
            _animator.SetTrigger(DISAPPEAR_ANIMATION_TRIGGER);
            _hasPlayedDisappearAnimation = true;
        } 
    }

}
