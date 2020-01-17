using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterStats))]
public class CharacterController : MonoBehaviour {

    public Action<CharacterController> onDead;
    public Action onTakeDamage;

    private CharacterMotor _characterMotor;
    private CharacterStats _characterStats;

    public bool IsMoving { get { return _characterMotor.IsMoving; } }
    public float CurrentHealth { get { return _characterStats.GetCurrentHealth(); } }
    public float MaxHealth { get { return _characterStats.GetMaxHealth(); } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Die() {
        onDead?.Invoke(this);
    }

    public void MoveToInput(Vector2 input) {
        _characterMotor.MoveToInput(input);
    }

    public void Crouch() {
        _characterMotor.Crouch();
    }

    public void StandUp() {
        _characterMotor.StandUp();
    }

    public void TakeDamage(float amount) {
        _characterStats.DecreaseHealth(amount);

        if (_characterStats.GetCurrentHealth() <= 0) {
            Die();
            return;
        }

        onTakeDamage?.Invoke();
    }

}
