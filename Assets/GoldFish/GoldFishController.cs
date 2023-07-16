using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishController : MonoBehaviour
{
    [Tooltip("�ړ����x")]
    [SerializeField]
    private float _moveSpeed = 1000f;

    [Tooltip("�ō����x�ɓ��B����܂ł̎���")]
    [SerializeField]
    private float _moveAcceleration = 10f;

    [Tooltip("���x���O�ɓ��B����܂ł̎���")]
    [SerializeField]
    private float _stopAcceleration = 10f;

    [Tooltip("��]���x")]
    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Transform _moveTransform;

    private float _currentMoveSpeed = 0f;

    private Vector2 _currentVeclocity = default;

    void Start()
    {
        
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        Vector2 input = GetInput();

        //���͂����邩�ǂ����m�F
        if (input != Vector2.zero)
        {
            var velocity = Vector2.up * input.y + Vector2.right * input.x;
            velocity = _moveSpeed * deltaTime * velocity.normalized;

            //�ړ��̑��x�����ʐ��`��Ԃ���
            _currentMoveSpeed += deltaTime / _moveAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0����1�͈̔͂ɃN�����v
            velocity = Vector3.Slerp(Vector2.zero, velocity, _currentMoveSpeed);

            _rb.velocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= deltaTime / _stopAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0����1�͈̔͂ɃN�����v
            _rb.velocity = Vector3.Slerp(Vector2.zero, _currentVeclocity, _currentMoveSpeed);
        }

        //���������X�ɕύX����
        if (_rb.velocity.sqrMagnitude > 0)
        {
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, targetRotation.eulerAngles.z, _rotationSpeed * Time.deltaTime));
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
