using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private PlayerStats_SO _playerDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
    private PlayerStats_SO _player = null;

    #region Initializations

    private void Awake() {
        if (_playerDefinition_Template != null) {
            _player = Instantiate(_playerDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void AddCurrentScore(int value) {
        _player.CurrentScore += value;

        if (_player.CurrentScore >= _player.HighScore) {
            SetHighScore(_player.CurrentScore);
        }
    }

    public void AddGold() {
        _player.Gold++;
    }

    #endregion

    #region Setters

    public void SetCurrentScore(int value) {
        _player.CurrentScore = value;

        if (_player.CurrentScore >= _player.HighScore) {
            SetHighScore(_player.CurrentScore);
        }
    }

    public void SetHighScore(int value) {
        _player.HighScore = value;
    }

    public void SetGold(int value) {
        _player.Gold = value;
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _playerDefinition_Template.Name;
    }

    public int GetCurrentScore() {
        return _player.CurrentScore;
    }

    public int GetHighScore() {
        return _player.HighScore;
    }

    public int GetGold() {
        return _player.Gold;
    }

    #endregion

}