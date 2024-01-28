using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum GameState
{
    GameSetup,
    InGame,
    GameOver
}

public class GMArena : MonoBehaviour
{
    [SerializeField, NaughtyAttributes.Scene] private string _mainMenuScene;

    [SerializeField] private TMP_Text _timerText;
    [Space]
    [SerializeField] private GameObject _kingFirstDialogPanel;
    [Space]
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private GameObject _newPlayerPanel;
    [Space]
    [SerializeField] private TMP_Text _gameOverPlayerNameText;
    [SerializeField] private GameObject _gameOverPanel;
    [Space]
    [SerializeField] private CrowdManager _crowdManager;
    [Space]
    [SerializeField] private PlayableDirector _arenaDirector;
    [SerializeField] private PlayableAsset _rightPlayerLooseSequence;
    [SerializeField] private PlayableAsset _leftPlayerLooseSequence;
    [SerializeField] private PlayableAsset _bothPlayersLooseSequence;
    [Space]
    [SerializeField] private float _phaseDuration = 10; // if not using turns will be match time limit.
    [SerializeField] private bool _useTurns;
    [SerializeField] private int _maxTurns = 2;

    [SerializeField] private Player[] _players;

    [Header("Debug")]
    [SerializeField] private float _phaseTimer = 0;
    [SerializeField] private int _currentTurn = -1;
    [SerializeField] private int _currentPlayer = -1;
    [SerializeField] private GameState _state;
    [SerializeField] private bool _waitingForPlayer = false;

    public GameState State { get { return _state; } }
    public bool BattleRunning { get { return _state == GameState.InGame && !_waitingForPlayer; } }


    private void Awake()
    {
        MessageReceiver.Instance.arenaGameManager = this;

        _newPlayerPanel.SetActive(false);
        _kingFirstDialogPanel.SetActive(false);
        _gameOverPanel.SetActive(false);

        _arenaDirector.playableAsset = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        EnterGameSetup();
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case GameState.GameSetup:
            break;
            case GameState.InGame:
                if(!_waitingForPlayer)
                {
                    if(_phaseTimer < 0)
                    {
                        if(_useTurns)
                        {
                            NextPlayer();
                        }
                        else
                        {
                            EnterGameOver(-1);
                        }
                    }
                    else
                    {
                        _phaseTimer -= Time.deltaTime;
                        _timerText.text = ((int)_phaseTimer).ToString();
                    }
                }
            break;
            case GameState.GameOver:
            break;
        }
    }


    void NewTurn()
    {
        if(_currentTurn == _maxTurns)
        {
            EnterGameOver(-1);
        }
        else
        {
            _currentTurn++;
            NewPlayerSetup();
        }
    }


    void NextPlayer()
    {
        if(_currentPlayer == _players.Length - 1)
        {
            _currentPlayer = 0;
            NewTurn();
        }
        else
        {
            _currentPlayer++;
            NewPlayerSetup();
        }
    }


    /// <summary>
    /// Ação para quando um novo jogador é definido.
    /// </summary>
    void NewPlayerSetup()
    {
        _waitingForPlayer = true;
        _playerNameText.text = _players[_currentPlayer].Name;
        _newPlayerPanel.SetActive(true);
    }


    #region States
    void EnterGameSetup()
    {
        _state = GameState.GameSetup;

        _kingFirstDialogPanel.SetActive(true);
    }


    void EnterInGame()
    {
        _state = GameState.InGame;

        _currentTurn = 0;
        _currentPlayer = 0;

        if(_useTurns)
        {
            NewPlayerSetup();
        }
        else
        {
            _waitingForPlayer = false;
            _phaseTimer = _phaseDuration;
            _timerText.gameObject.SetActive(true);
            _timerText.text = _phaseTimer.ToString();
        }
    }

    
    void EnterGameOver(int looserIndex)
    {
        _state = GameState.GameOver;

        if(looserIndex < 0)
        {
            _gameOverPlayerNameText.text = "Draw";

            _arenaDirector.playableAsset = _bothPlayersLooseSequence;
            _arenaDirector.Play();
        }
        else
        {
            if(looserIndex == 0)
            {
                _arenaDirector.playableAsset = _leftPlayerLooseSequence;
            }
            else
            {
                _arenaDirector.playableAsset = _rightPlayerLooseSequence;
            }

            _arenaDirector.Play();
        }
    }
    #endregion

    #region Buttons
    public void ButtonStartGame()
    {
        // Executado no inicio apos dialogo do rei para inciar o jogo.
        _kingFirstDialogPanel.SetActive(false);
        EnterInGame();
    }

    public void ButtonStartCurrentPlayerPhase()
    {
        _waitingForPlayer = false;
        _newPlayerPanel.SetActive(false);

        _phaseTimer = _phaseDuration;
        _timerText.gameObject.SetActive(true);
        _timerText.text = _phaseTimer.ToString();
    }

    public void ButtonStartTwoPlayersPhase()
    {
        _waitingForPlayer = false;

    }

    public void ButtonRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ButtonBackToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }


    public void ButtonQuit()
    {
        Application.Quit();
    }
    #endregion


    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }


    public void PlayerSmiled(string value)
    {
        if(BattleRunning)
        {
            EnterGameOver(_currentPlayer);
        }
    }
}
