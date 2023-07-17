using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishController : MonoBehaviour
{
    [Tooltip("移動速度")]
    [SerializeField]
    private float _moveSpeed = 1000f;

    [Tooltip("最高速度に到達するまでの時間")]
    [SerializeField]
    private float _moveAcceleration = 10f;

    [Tooltip("速度が０に到達するまでの時間")]
    [SerializeField]
    private float _stopAcceleration = 10f;

    [Tooltip("回転速度")]
    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Transform _moveTransform;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _eat;

    public float _currentMoveSpeed = 0f;

    private Vector2 _currentVeclocity = default;

    private InGameController _controller = default;


    void Start()
    {
        _controller = FindObjectOfType<InGameController>().GetComponent<InGameController>();
    }

    void Update()
    {
        if (_controller != null)
        {
            if (_controller.State != InGameController.InGameState.Game)
            {
                _rb.Sleep();
                return;
            }
            else
            {
                _rb.WakeUp();
            }
        }

        float deltaTime = Time.deltaTime;

        Vector2 input = GetInput();

        //入力があるかどうか確認
        if (input != Vector2.zero)
        {
            var velocity = Vector2.up * input.y + Vector2.right * input.x;
            velocity = _moveSpeed * velocity.normalized;

            //移動の速度を球面線形補間する
            _currentMoveSpeed += deltaTime / _moveAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0から1の範囲にクランプ
            velocity = Vector3.Slerp(Vector2.zero, velocity, _currentMoveSpeed);

            _rb.velocity = velocity;
            _currentVeclocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= deltaTime / _stopAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0から1の範囲にクランプ
            _rb.velocity = Vector3.Slerp(Vector2.zero, _currentVeclocity, _currentMoveSpeed);
        }

        //向きを徐々に変更する
        if (_rb.velocity.sqrMagnitude > 0)
        {
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, targetRotation.eulerAngles.z, _rotationSpeed * Time.deltaTime));
        }

        _anim?.SetFloat("Input", input.sqrMagnitude);
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Insect"))
        {
            _audioSource.clip = _eat;
            _audioSource.Play();
        }
    }
}
