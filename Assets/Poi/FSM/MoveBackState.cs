using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߂� �����ʒu�ɓ��B�� ���b�Z�[�W���M
/// </summary>
public class MoveBackState : PoiStateBase
{
    /// <summary>
    /// �ʒu���قړ������𔻒肷��p
    /// </summary>
    const float Threshold = 0.005f;
    /// <summary>
    /// �ړ����x
    /// </summary>
    const float Speed = 6.0f;
    /// <summary>
    /// ������̑ҋ@����
    /// </summary>
    const float Delay = 1.0f;

    /// <summary>
    /// ������̑ҋ@���Ԃ��J�E���g����
    /// </summary>
    float _timer;

    public MoveBackState(PoiStateType type, PoiBlackBoard blackBoard) : base(type, blackBoard) { }

    protected override void Enter()
    {
        _timer = 0;
    }

    protected override void Exit()
    {
    }

    protected override void Stay()
    {
        Vector2 dirVector = BlackBoard.InitPos - (Vector2)BlackBoard.transform.position;
        // ����
        if (dirVector.sqrMagnitude <= Threshold)
        {
            // ���΂炭�҂�
            _timer += Time.deltaTime;
            if (_timer > Delay)
            {
                Debug.Log("�����");
                BlackBoard.gameObject.SetActive(false);
            }
        }
        // ������
        else
        {
            // �ړ�
            BlackBoard.transform.Translate(dirVector.normalized * Time.deltaTime * Speed);
        }
    }
}