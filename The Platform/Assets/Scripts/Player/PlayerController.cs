﻿using UnityEngine;

[RequireComponent(typeof(CharacterManager))]
public class PlayerController : MonoBehaviour {

    public Vector2 CurrentInput { get; set; }

    public bool IsCrouching {
        get { return _isCrouching; }
        set { _isCrouching = value; }
    }

    public bool HasInput { get { return (CurrentInput != Vector2.zero) ? true : false; } }

    public CharacterManager CharacterController { get { return _characterController; } }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _xInput;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isJumping;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isCrouching;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterManager _characterController;

    private void Awake() {
        _characterController = GetComponent<CharacterManager>();
    }

    private void Update() {
        _isJumping = IsJumping();

        _xInput = Input.GetAxis("Horizontal");

        CurrentInput = new Vector2(_xInput, 0f);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (!IsCrouching) {
                Crouch();
            }
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
            if (IsCrouching) {
                StandUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (!IsJumping()) {
                Jump();
            }
        }

        if (HasInput && !IsCrouching) {
            MoveToCurrentInput();
        }
    }

    public void Jump() {
        _characterController.Jump();
    }

    public void Crouch() {
        IsCrouching = true;
        _characterController.Crouch();
    }

    public void StandUp() {
        IsCrouching = false;
        _characterController.StandUp();
    }

    public bool IsJumping() {
        return _characterController.IsJumping;
    }

    public void MoveToCurrentInput() {
        _characterController.MoveToInput(CurrentInput);
    }

}
