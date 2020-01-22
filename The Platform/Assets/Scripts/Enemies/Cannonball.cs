using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour, IPooledObject {

    public enum Direction {
        RIGHT,
        LEFT
    }

    [Header("Initializations")]
    [SerializeField]
    private Rigidbody _rb = null;
    [SerializeField]
    private float _fireAngle = 45f;
    [SerializeField]
    private Renderer _renderer = null;
    [SerializeField]
    private TrailRenderer _trailRenderer = null;
    [SerializeField]
    private ParticleSystem _crashFX = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Enemy _enemy = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Direction _direction = Direction.LEFT;
    [SerializeField]
    [Utils.ReadOnly]
    private Color _generatedRandomColor;

    private void Awake() {
        _enemy = GetComponent<Enemy>();
    }

    private void Start() {
        SetRandomColor();
        SetTrailRendererColor();
        SetCrashParticleColor();
    }

    private void Update() {
        if (_rb.position.y < 0) {
            _rb.transform.position = new Vector3(_rb.transform.position.x, 0f, _rb.transform.position.z);
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

            _renderer.enabled = false;
            _crashFX.Play();

            _enemy.OnCrashed();
        }
    }

    public void SetRandomColor() {
        _generatedRandomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        _renderer.material.color = _generatedRandomColor;
    }

    public void SetTrailRendererColor() {
        float alpha = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(_generatedRandomColor, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        _trailRenderer.colorGradient = gradient;
    }

    public void SetCrashParticleColor() {
        ParticleSystem.MainModule main = _crashFX.main;
        main.startColor = _generatedRandomColor;
    }

    private Vector3 GetRandomTargetPosition() {
        return new Vector3(0f, 0f, Random.Range(GameManager.instance.LeftMapPivotTransform.position.z, GameManager.instance.RightMapPivotTransform.position.z));
    }

    private void Fire() {
        Vector3 fireVector = HelperArcProjectile.MagicShoot(_fireAngle, GetRandomTargetPosition(), this.transform.position);
        _rb.AddForce(fireVector, ForceMode.VelocityChange);
    }

    public void SetDirection(Direction direction) {
        this._direction = direction;
    }

    public void OnObjectReused() {
        SetRandomColor();
        SetTrailRendererColor();
        SetCrashParticleColor();

        _renderer.enabled = true;
        _rb.isKinematic = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        this.gameObject.SetActive(true);

        Fire();
    }

}
