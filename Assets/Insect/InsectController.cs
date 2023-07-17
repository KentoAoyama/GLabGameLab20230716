using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class InsectController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _fly;

    [SerializeField]
    private AudioClip _drop;

    private InGameController _controller;

    public void Generate(Transform generatePos, Vector3 movePos, InGameController inGameController)
    {
        _audioSource.loop = true;
        _audioSource.clip = _fly;
        _audioSource.Play();

        transform.position = generatePos.position;

        var dir = movePos - generatePos.position;
        transform.up = dir;

        transform
            .DOMove(movePos, _speed)
            .OnComplete(() =>
            {
                _audioSource.loop = false;
                _audioSource.clip = _drop;
                _audioSource.Play();
            })
            .SetEase(Ease.Linear)
            .SetLink(gameObject);

        _controller = inGameController;
    }

    private void Update()
    {
        if (_controller.State != InGameController.InGameState.Game)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.AddScore();
            Destroy(gameObject);
        }
    }
}
