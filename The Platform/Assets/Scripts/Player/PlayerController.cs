using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public Vector2 CurrentInput { get; set; }

    public bool IsDucking { get; set; }

    public bool HasInput { get { return (CurrentInput != Vector2.zero) ? true : false; } }

    public CharacterController CharacterController { get { return _characterController; } }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _xInput;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _characterController;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        _xInput = Input.GetAxis("Horizontal");

        CurrentInput = new Vector2(_xInput, 0f);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (!IsDucking) {
                Crouch();
            }
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
            if (IsDucking) {
                StandUp();
            }
        }

        if (HasInput) {
            MoveToCurrentInput();
        }
    }

    public void Crouch() {
        IsDucking = true;
        _characterController.Crouch();
    }

    public void StandUp() {
        IsDucking = false;
        _characterController.StandUp();
    }

    public void MoveToCurrentInput() {
        _characterController.MoveToInput(CurrentInput);
    }

}
