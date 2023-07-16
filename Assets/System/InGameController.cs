using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    public enum InGameState
    {
        WaitStart,
        Game,
        Finish
    }

    private InGameState _state = InGameState.WaitStart;
    /// <summary>
    /// ゲームの状態を表す
    /// </summary>
    public InGameState State => _state;

    [Tooltip("ゲームの制限時間")]
    [SerializeField]
    private float _gameTime = 90f;

    [Tooltip("開始前のカウントダウンの時間")]
    [SerializeField]
    private float _countDownTime = 3f;

    private float _gameTimer = 0f;
    /// <summary>
    /// ゲームの制限時間
    /// </summary>
    public float Timer => _gameTimer;
    
    private float _countDownTimer;
    /// <summary>
    /// カウントダウンの時間
    /// </summary>
    public float StartTimer => _countDownTimer;

    private int _score = 0;
    /// <summary>
    /// スコアの値
    /// </summary>
    public int Score => _score;


    [Header("UI系の参照")]
    [SerializeField]
    private Text _timerText = default;

    [SerializeField]
    private Text _countDownText = default;

    [SerializeField]
    private Text _scoreText = default;

    private void Start()
    {
        _state = InGameState.WaitStart;

        ValueInitialized();
    }

    private void ValueInitialized()
    {
        _gameTimer = _gameTime;
        _countDownTimer = _countDownTime;
        _score = 0;
    }

    private void Update() 
    {
        float deltaTime = Time.deltaTime;

        switch (_state)
        {
            case InGameState.WaitStart:
                WaitStartStateUpdate(deltaTime);
                break;
            case InGameState.Game:
                GameStateUpdate(deltaTime);
                break;
            case InGameState.Finish:
                FinishStateUpdate(deltaTime);
                break;
        }
    }

    private void WaitStartStateUpdate(float deltaTime)
    {
        _countDownTimer -= deltaTime;
        Mathf.Clamp(_countDownTimer, 0f, _countDownTime);
    }

    private void GameStateUpdate(float deltaTime)
    {

    }

    private void FinishStateUpdate(float deltaTime)
    {

    }
}
