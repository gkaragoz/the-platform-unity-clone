using System;
using TMPro;
using UnityEngine;

public class PanelGameplayMenu : MonoBehaviour {

    [Header("Intializations")]
    [SerializeField]
    private TextMeshProUGUI _txtScore = null;
    [SerializeField]
    private TextMeshProUGUI _txtHighScore = null;

    private const string SCORE = "Score:\t";
    private const string HIGH_SCORE = "High Score:\t";

    private void Awake() {
        // TODO GET FROM DB.
        _txtScore.text = SCORE + 0;
        _txtHighScore.text = HIGH_SCORE + 0;
    }

    private void Start() {
        GameManager.instance.OnPlayerStatsChanged += OnPlayerStatsChanged;
    }

    private void OnPlayerStatsChanged(PlayerStats playerStats) {
        _txtScore.text = SCORE + playerStats.GetCurrentScore().ToString();
        _txtHighScore.text = HIGH_SCORE + playerStats.GetHighScore().ToString();
    }
}
