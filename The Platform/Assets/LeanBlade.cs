using UnityEngine;

public class LeanBlade : MonoBehaviour {

    [System.Serializable]
    public class Settings {
        public float placementAnimationTime = 0.5f;
        public float disappearAnimationTime = 0.5f;
        public float placementPositionY = 0f;
        public float disappearPositionY = -0.6f;
    }

    [Header("Initializations")]
    [SerializeField]
    private float _time = 2f;
    [SerializeField]
    private Transform _destinationTransform = null;
    [SerializeField]
    private Settings _settings = null;

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
        LeanTween.moveLocalY(this.gameObject, _settings.placementPositionY, _settings.placementAnimationTime).setEase(LeanTweenType.easeInOutQuad);
    }

    private void PlayDisappearAnimation() {
        LeanTween.moveLocalY(this.gameObject, _settings.disappearPositionY, _settings.disappearAnimationTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(SetActiveFalse);

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
