using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanBlade : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private float _time = 2f;
    [SerializeField]
    private Transform _destinationTransform = null;

    private const string PLACEMENT_ANIMATION_TRIGGER = "PLACEMENT_ANIMATION";
    private const string DISAPPEAR_ANIMATION_TRIGGER = "DISAPPEAR_ANIMATION";

    private bool _isMoving = false;
    private bool _hasPlayedDisappearAnimation = false;

    private void Update() {
        if (HasDestination() && !HasDestinationReached() && _isMoving == false) {
            this.gameObject.SetActive(true);
            _hasPlayedDisappearAnimation = false;

            PlayPlacementAnimation();
            MoveToDestination();
        } else if (HasDestination() && HasDestinationReached() && _hasPlayedDisappearAnimation == false) {
            PlayDisappearAnimation();
            _isMoving = false;
        }
    }

    private bool HasDestination() {
        return _destinationTransform == null ? false : true;
    }

    private bool HasDestinationReached() {
        return Mathf.Abs(transform.position.z - _destinationTransform.position.z) <= 0.001f ? true : false;
    }

    private void PlayPlacementAnimation() {
        _animator.SetTrigger(PLACEMENT_ANIMATION_TRIGGER);
    }

    private void PlayDisappearAnimation() {
        _animator.SetTrigger(DISAPPEAR_ANIMATION_TRIGGER);
        _hasPlayedDisappearAnimation = true;
    }

    private void MoveToDestination() {
        _isMoving = true;
        LeanTween.move(this.gameObject, new Vector3(transform.position.x, transform.position.y, _destinationTransform.position.z), _time).setEase(LeanTweenType.easeInOutQuad);
    }

    public void SetActiveFalse() {
        this.gameObject.SetActive(false);
    }

}
