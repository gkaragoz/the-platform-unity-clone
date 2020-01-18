using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class LeanBlade : MonoBehaviour, IPooledObject {

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
    private Settings _settings = null;

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

    private void PlayPlacementAnimation() {
        LeanTween.moveLocalY(this.gameObject, _settings.placementPositionY, _settings.placementAnimationTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(MoveToDestination);
    }

    private void PlayDisappearAnimation() {
        LeanTween.moveLocalY(this.gameObject, _settings.disappearPositionY, _settings.disappearAnimationTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(_enemy.OnCrashed);
    }

    private void MoveToDestination() {
        LeanTween.move(this.gameObject, new Vector3(transform.position.x, transform.position.y, _destinationTransform.position.z), _time).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlayDisappearAnimation);
    }

    public void SetDestinationTransform(Transform destination) {
        this._destinationTransform = destination;
    }

    public void OnObjectReused() {
        PlayPlacementAnimation();

        gameObject.SetActive(true);
    }

}
