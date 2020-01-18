using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler), typeof(LeanBlade), typeof(EnemyGenerator))]
public class GameManager : MonoBehaviour {
    
    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public enum GameState {
        MainMenu,
        GamePaused,
        NewGame,
        RestartGame,
        GameplayCountdown,
        Gameplay,
        GameOver
    }

    public Action<GameState> OnGameStateChanged;
    public Action<PlayerStats> OnPlayerStatsChanged;

    [Header("Initializations")]
    [SerializeField]
    private EnemyGenerator _enemyGenerator = null;
    [SerializeField]
    private PlayerController _playerController = null;

    [SerializeField]
    private Transform _leftMapPivotTransform = null;
    [SerializeField]
    private Transform _rightMapPivotTransform = null;
    [SerializeField]
    private Transform _rockFallPivotTransform = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private GameState _gameState = GameState.MainMenu;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _hasGameObjectsInitialized = false;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isGamePaused = false;

    public GameState GameStateEnum { 
        get {
            return _gameState;
        }
        private set {
            _gameState = value;
            Debug.Log(">>GAME STATE HAS BEEN CHANGED: " + _gameState.ToString());
            OnGameStateChanged?.Invoke(_gameState);
        } 
    }
    public Transform LeftMapPivotTransform { get { return _leftMapPivotTransform; } }
    public Transform RightMapPivotTransform { get { return _rightMapPivotTransform; } }
    public Transform RockFallPivotTransform { get { return _rockFallPivotTransform; } }

    private void InitializeNewGame() {
        if (_isGamePaused) {
            Unpause();
        }

        if (!_hasGameObjectsInitialized) {
            ObjectPooler.instance.InitializePool("Rock");
            ObjectPooler.instance.InitializePool("BladeForward");
            ObjectPooler.instance.InitializePool("BladeBackward");

            InitializeBladeDestinations();

            _hasGameObjectsInitialized = true;
        }
    }
    private void InitializeBladeDestinations() {
        foreach (GameObject bladeObject in ObjectPooler.instance.GetGameObjectsOnPool("BladeForward")) {
            bladeObject.GetComponent<LeanBlade>().SetDestinationTransform(_rightMapPivotTransform);
        }
        foreach (GameObject bladeObject in ObjectPooler.instance.GetGameObjectsOnPool("BladeBackward")) {
            bladeObject.GetComponent<LeanBlade>().SetDestinationTransform(_leftMapPivotTransform);
        }
    }

    private IEnumerator IStartGameCountdown() {
        GameStateEnum = GameState.GameplayCountdown;

        yield return new WaitForSeconds(3f);

        StartGame();
    }

    private void StartGame() {
        _enemyGenerator.StartGenerateAll();

        GameStateEnum = GameState.Gameplay;
    }

    private void Pause() {
        Time.timeScale = 0;

        _isGamePaused = true;

        GameStateEnum = GameState.GamePaused;
    }

    private void Unpause() {
        Time.timeScale = 1;

        _isGamePaused = false;

        GameStateEnum = GameState.Gameplay;
    }

    public void OnClick_NewGame() {
        InitializeNewGame();

        GameStateEnum = GameState.NewGame;

        StartCoroutine(IStartGameCountdown());
    }

    public void OnClick_PauseUnpauseGame() {
        _isGamePaused = !_isGamePaused;
        
        if (_isGamePaused) {
            Pause();
        } else {
            Unpause();
        }
    }

    public void OnClick_RestartGame() {
        OnClick_NewGame();

        GameStateEnum = GameState.RestartGame;
    }

    public void AddScoreToPlayer(int value) {
        _playerController.AddScore(value);

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }

}
