using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private CharacterStats_SO _characterDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
    private CharacterStats_SO _character = null;

    #region Initializations

    private void Awake() {
        if (_characterDefinition_Template != null) {
            _character = Instantiate(_characterDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void IncreaseHealth(float value) {
        if (GetCurrentHealth() + value >= GetMaxHealth()) {
            return;
        }

        _character.CurrentHealth += value;
    }

    #endregion

    #region Decreasers

    public void DecreaseHealth(float value) {
        _character.CurrentHealth -= value;

        if (GetCurrentHealth() <= 0) {
            _character.CurrentHealth = 0;
        }
    }

    #endregion

    #region Setters

    public void SetCurrentHealth(float amount) {
        if (amount <= 0) {
            _character.CurrentHealth = 0;
            return;
        }
        if (amount > GetMaxHealth()) {
            _character.MaxHealth = amount;
        }

        _character.CurrentHealth = amount;
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _characterDefinition_Template.Name;
    }

    public GameObject GetPrefab() {
        return _characterDefinition_Template.Prefab;
    }

    public float GetCurrentHealth() {
        return _character.CurrentHealth;
    }

    public float GetMaxHealth() {
        return _character.MaxHealth;
    }

    public float GetMovementSpeed() {
        return _character.MovementSpeed;
    }

    public float GetJumpPower() {
        return _character.JumpPower;
    }

    public int GetLevel() {
        return _character.Level;
    }

    public int GetMaxExperience() {
        return _character.MaxExperience;
    }

    public int GetCurrentExperience() {
        return _character.CurrentExperience;
    }

    #endregion

    #region Custom Methods

    private void LevelUp() {
        _character.Level++;

        _character.CurrentExperience = 0;
        _character.MaxExperience = 10 + (_character.Level * 10) + (int)Mathf.Pow(_character.Level + 1, 3);
    }

    #endregion

}