using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GUIController
{
    [Header("UIŒn‚ÌŽQÆ")]
    [SerializeField]
    private Text _timerText = default;
    public Text TimerText => _timerText;

    [SerializeField]
    private Text _countDownText = default;
    public Text CountDownText => _countDownText;

    [SerializeField]
    private Text _scoreText = default;
    public Text ScoreText => _scoreText;

    [SerializeField]
    private Text _titleText = default;

    [SerializeField]
    private Text _resultText = default;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _start;

    public void Clear()
    {
        if (_timerText != null)
            _timerText.gameObject.SetActive(false);

        if (_countDownText != null)
            _countDownText.gameObject.SetActive(false);

        if (_scoreText != null)
            _scoreText.gameObject.SetActive(false);

        if (_timerText != null)
            _titleText.gameObject.SetActive(false);

        if (_resultText != null)
            _resultText.gameObject.SetActive(false);
    }

    public void Initialized()
    {
        if (_timerText != null)
            _timerText.gameObject.SetActive(false);

        if (_countDownText != null)
            _countDownText.gameObject.SetActive(false);

        if (_scoreText != null)
            _scoreText.gameObject.SetActive(false);

        if (_timerText != null)
            _titleText.gameObject.SetActive(true);

        if (_resultText != null)
            _resultText.gameObject.SetActive(false);
    }

    public void SetTitleText(bool isActive)
    {
        if (_titleText == null) return;

        _resultText.gameObject.SetActive(false);
        _titleText.gameObject.SetActive(isActive);
    }

    public void SetCountDownTime(int time)
    {
        time--;

        if (time > 0)
        {
            CountDownText.text = time.ToString();
        }
        else
        {
            CountDownText.text = "ŠJŽn²!!!";

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _start;
                _audioSource.Play();
            }
        }
    }

    public void SetGameTimer(int time)
    {
        _timerText.text = time.ToString("00");
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString("00");
    }

    public void SetResult(int score, bool isActive)
    {
        CountDownText.gameObject.SetActive(isActive);
        CountDownText.text = score.ToString();
        _resultText.gameObject.SetActive(isActive);
    }
}
