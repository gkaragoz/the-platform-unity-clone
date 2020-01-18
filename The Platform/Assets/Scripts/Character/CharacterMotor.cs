using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    private CharacterStats _characterStats;
    private Rigidbody _rb;

    [Header("Initializations")]
    [SerializeField]
    private Transform _meshGraphicsTransform = null;
    [SerializeField]
    private Transform _colliderPivotTransform = null;
    [SerializeField]
    private Transform _footRayPivotTransform = null;
    [SerializeField]
    private float _crouchDownSpeedMultiplier = 1f;

    public bool IsMoving { get { return _rb.velocity.magnitude > 0.01f ? true : false; } }
    public float VelocityMagnitude { get { return _rb.velocity.magnitude; } }

    private Coroutine ISetCrouchDownVelocityCoroutine;

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
        _rb = GetComponent<Rigidbody>();
    }

    public void MoveToInput(Vector2 input) {
        _rb.velocity = new Vector3(0f, _rb.velocity.y, input.x * _characterStats.GetMovementSpeed());
    }

    public void Jump() {
        _rb.AddForce(Vector3.up * _characterStats.GetJumpPower());
    }

    public void Crouch() {
        _meshGraphicsTransform.localPosition = new Vector3(_meshGraphicsTransform.localPosition.x, 1.0f, _meshGraphicsTransform.localPosition.z);
        _colliderPivotTransform.localScale = new Vector3(_meshGraphicsTransform.localScale.x, 0.75f, _meshGraphicsTransform.localScale.z);

        if (ISetCrouchDownVelocityCoroutine == null) {
            ISetCrouchDownVelocityCoroutine = StartCoroutine(ISetCrouchDownVelocity());
        }
    }

    public void StandUp() {
        _meshGraphicsTransform.localPosition = new Vector3(_meshGraphicsTransform.localPosition.x, 1.5f, _meshGraphicsTransform.localPosition.z);
        _colliderPivotTransform.localScale = new Vector3(_meshGraphicsTransform.localScale.x, 1.0f, _meshGraphicsTransform.localScale.z);
    }
    public bool GetIsJumping() {
        RaycastHit hit;
        if (Physics.Raycast(_footRayPivotTransform.position, Vector3.down, out hit, Mathf.Infinity)) {
            float distance = Vector3.Distance(_footRayPivotTransform.position, hit.point);
            return distance <= 0.001f ? false : true;
        }
        return true;
    }

    private IEnumerator ISetCrouchDownVelocity() {
        float tempMultiplier = _crouchDownSpeedMultiplier;

        while (_rb.velocity.y != 0) {
            _rb.velocity = tempMultiplier * Vector3.down;

            yield return new WaitForEndOfFrame();

            tempMultiplier -= Time.deltaTime;
        }

        ISetCrouchDownVelocityCoroutine = null;
    }

}