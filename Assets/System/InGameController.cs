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
    /// �Q�[���̏�Ԃ�\��
    /// </summary>
    public InGameState State => _state;

    [Tooltip("�Q�[���̐�������")]
    [SerializeField]
    private float _gameTime = 90f;

    [Tooltip("�J�n�O�̃J�E���g�_�E���̎���")]
    [SerializeField]
    private float _countDownTime = 3f;

    private float _gameTimer = 0f;
    /// <summary>
    /// �Q�[���̐�������
    /// </summary>
    public float Timer => _gameTimer;
    
    private float _countDownTimer;
    /// <summary>
    /// �J�E���g�_�E���̎���
    /// </summary>
    public float StartTimer => _countDownTimer;

    private int _score = 0;
    /// <summary>
    /// �X�R�A�̒l
    /// </summary>
    public int Score => _score;


    [Header("UI�n�̎Q��")]
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
