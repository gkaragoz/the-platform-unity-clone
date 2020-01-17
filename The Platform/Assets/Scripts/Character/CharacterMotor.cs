using UnityEngine;

[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    private CharacterStats _characterStats;
    private Rigidbody _rb;

    [Header("Initializations")]
    [SerializeField]
    private Transform _meshGraphicsTransform;
    [SerializeField]
    private Transform _colliderPivotTransform;

    public bool IsMoving { get { return _rb.velocity.magnitude > 0.01f ? true : false; } }
    public float VelocityMagnitude { get { return _rb.velocity.magnitude; } }

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
        _rb = GetComponent<Rigidbody>();
    }

    public void MoveToInput(Vector2 input) {
        _rb.velocity = new Vector3(0f, 0f, input.x) * _characterStats.GetMovementSpeed();
    }

    public void Crouch() {
        _meshGraphicsTransform.localPosition = new Vector3(_meshGraphicsTransform.localPosition.x, 0.5f, _meshGraphicsTransform.localPosition.z);
        _colliderPivotTransform.localScale = new Vector3(_meshGraphicsTransform.localScale.x, 0.5f, _meshGraphicsTransform.localScale.z);
    }

    public void StandUp() {
        _meshGraphicsTransform.localPosition = new Vector3(_meshGraphicsTransform.localPosition.x, 1.5f, _meshGraphicsTransform.localPosition.z);
        _colliderPivotTransform.localScale = new Vector3(_meshGraphicsTransform.localScale.x, 1.0f, _meshGraphicsTransform.localScale.z);
    }

}