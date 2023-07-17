using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    public enum InGameState
    {
        Start,
        Title,
        CountDown,
        Game,
        Finish
    }

    [SerializeField] // Debug用　後で消す
    private InGameState _state = InGameState.CountDown;
    /// <summary>
    /// ゲームの状態を表す
    /// </summary>
    public InGameState State => _state;

    [Tooltip("GUIの管理をするクラス")]
    [SerializeField]
    private GUIController _gui;

    [SerializeField]
    private float _startInterval = 2f;

    [SerializeField]
    private PlayerHitMessageReceiver _receiver;

    [Tooltip("ゲームの制限時間")]
    [SerializeField]
    private float _gameTime = 90f;

    [Tooltip("開始前のカウントダウンの時間")]
    [SerializeField]
    private float _countDownTime = 3f;

    /// <summary>
    /// ゲームの制限時間を計測する変数
    /// </summary>
    private float _gameTimer = 0f;

    /// <summary>
    /// カウントダウンの時間を計測する変数
    /// </summary>
    private float _countDownTimer = 0f;

    /// <summary>
    /// スコアの値
    /// </summary>
    private static int _score = 0;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _button;

    [SerializeField]
    private AudioClip _finish;

    [SerializeField]
    private AudioClip _eat;

    [SerializeField]
    private Animator _animator;

    private float _startTimer = 0f;

    private void Start()
    {
        if (_receiver != null)
            _receiver.OnPlayerHit += TransitionToFinishState;

        _gui.Initialized();
        _gui.Clear();
    }

    private void ValueInitialized()
    {
        _state = InGameState.Title;
        _gameTimer = _gameTime + 1;
        _countDownTimer = _countDownTime + 1;
        _score = 0;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        switch (_state)
        {
            case InGameState.Start:
                StartUpdate(deltaTime);
                break;
            case InGameState.Title:
                TitleStateUpdate();
                break;
            case InGameState.CountDown:
                CountDownStateUpdate(deltaTime);
                break;
            case InGameState.Game:
                GameStateUpdate(deltaTime);
                break;
            case InGameState.Finish:
                FinishStateUpdate();
                break;
        }
    }

    private void StartUpdate(float deltaTime)
    {
        _startTimer += deltaTime;

        if (_startTimer > _startInterval)
        {
            TransitionToTitleState();
        }
    }

    /// <summary>
    /// タイトル画面に遷移する処理。ボタンとかで呼ぶ想定
    /// </summary>
    public void TransitionToTitleState()
    {
        Debug.Log("TitleStateになりました");

        _animator.enabled = false;

        _audioSource.clip = _button;
        _audioSource.Play();

        ValueInitialized();
        _state = InGameState.Title;

        _gui.SetTitleText(true);
    }

    private void TitleStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransitionToCountDownState();
        }
    }

    /// <summary>
    /// カウントダウンに遷移する処理。ボタンとかで呼ぶ想定
    /// </summary>
    public void TransitionToCountDownState()
    {

        Debug.Log("CountDownになりました");
        _state = InGameState.CountDown;

        _audioSource.clip = _button;
        _audioSource.Play();

        _gui.SetTitleText(false);
        _gui.CountDownText.gameObject.SetActive(true);
    }

    private void CountDownStateUpdate(float deltaTime)
    {
        //カウントダウンのタイマーを減算する
        _countDownTimer -= deltaTime;
        _gui.SetCountDownTime((int)_countDownTimer + 1);

        if (_countDownTimer < 0.4)
        {
            TransitionToGameState();
        }
    }

    private void TransitionToGameState()
    {
        Debug.Log("GameStateになりました");
        _state = InGameState.Game;

        _gui.CountDownText.gameObject.SetActive(false);
        _gui.TimerText.gameObject.SetActive(true);
        _gui.ScoreText.gameObject.SetActive(true);

        PoiGenerateController.Start();
        InsectGenerator.GenerateStart();
    }

    private void GameStateUpdate(float deltaTime)
    {
        //ゲームの制限時間を減算する
        _gameTimer -= deltaTime;
        _gui.SetGameTimer((int)_gameTimer);
        _gui.SetScore(_score);

        if (_gameTimer < 0)
        {
            TransitionToFinishState();
        }
    }

    private void TransitionToFinishState()
    {
        Debug.Log("FinishStateになりました");
        _state = InGameState.Finish;

        _audioSource.clip = _finish;
        _audioSource.Play();

        _gui.TimerText.gameObject.SetActive(false);
        _gui.ScoreText.gameObject.SetActive(false);
        _gui.SetResult(_score, true);

        PoiGenerateController.Stop();
        InsectGenerator.GenerateStop();
    }

    private void FinishStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransitionToTitleState();
            _gui.SetResult(0, false);
        }
    }

    public static void AddScore(int score = 1)
    {
        _score += score;
    }
}
