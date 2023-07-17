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

    [SerializeField] // Debug�p�@��ŏ���
    private InGameState _state = InGameState.CountDown;
    /// <summary>
    /// �Q�[���̏�Ԃ�\��
    /// </summary>
    public InGameState State => _state;

    [Tooltip("GUI�̊Ǘ�������N���X")]
    [SerializeField]
    private GUIController _gui;

    [SerializeField]
    private float _startInterval = 2f;

    [SerializeField]
    private PlayerHitMessageReceiver _receiver;

    [Tooltip("�Q�[���̐�������")]
    [SerializeField]
    private float _gameTime = 90f;

    [Tooltip("�J�n�O�̃J�E���g�_�E���̎���")]
    [SerializeField]
    private float _countDownTime = 3f;

    /// <summary>
    /// �Q�[���̐������Ԃ��v������ϐ�
    /// </summary>
    private float _gameTimer = 0f;

    /// <summary>
    /// �J�E���g�_�E���̎��Ԃ��v������ϐ�
    /// </summary>
    private float _countDownTimer = 0f;

    /// <summary>
    /// �X�R�A�̒l
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
    /// �^�C�g����ʂɑJ�ڂ��鏈���B�{�^���Ƃ��ŌĂԑz��
    /// </summary>
    public void TransitionToTitleState()
    {
        Debug.Log("TitleState�ɂȂ�܂���");

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
    /// �J�E���g�_�E���ɑJ�ڂ��鏈���B�{�^���Ƃ��ŌĂԑz��
    /// </summary>
    public void TransitionToCountDownState()
    {

        Debug.Log("CountDown�ɂȂ�܂���");
        _state = InGameState.CountDown;

        _audioSource.clip = _button;
        _audioSource.Play();

        _gui.SetTitleText(false);
        _gui.CountDownText.gameObject.SetActive(true);
    }

    private void CountDownStateUpdate(float deltaTime)
    {
        //�J�E���g�_�E���̃^�C�}�[�����Z����
        _countDownTimer -= deltaTime;
        _gui.SetCountDownTime((int)_countDownTimer + 1);

        if (_countDownTimer < 0.4)
        {
            TransitionToGameState();
        }
    }

    private void TransitionToGameState()
    {
        Debug.Log("GameState�ɂȂ�܂���");
        _state = InGameState.Game;

        _gui.CountDownText.gameObject.SetActive(false);
        _gui.TimerText.gameObject.SetActive(true);
        _gui.ScoreText.gameObject.SetActive(true);

        PoiGenerateController.Start();
        InsectGenerator.GenerateStart();
    }

    private void GameStateUpdate(float deltaTime)
    {
        //�Q�[���̐������Ԃ����Z����
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
        Debug.Log("FinishState�ɂȂ�܂���");
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
