using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O�i �����サ�΂炭�ҋ@���Ď��̏�Ԃ�
/// </summary>
public class MoveForwardState : PoiStateBase
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
    const float Delay = 0.5f;

    Color _baseColor;
    float _currentSpeed;
    /// <summary>
    /// ������̑ҋ@���Ԃ��J�E���g����
    /// </summary>
    float _timer;

    bool _isAudioPlayed;

    public MoveForwardState(PoiStateType type, PoiBlackBoard blackBoard) : base(type, blackBoard) { }

    protected override void Enter()
    {
        _baseColor = BlackBoard.PoiSprite.color;
        _currentSpeed = Speed;
        _timer = 0;
        _isAudioPlayed = false;
    }

    protected override void Exit()
    {
    }

    protected override void Stay()
    {
        Vector2 dirVector = BlackBoard.TargetPos - (Vector2)BlackBoard.transform.position;
        // ����
        if(dirVector.sqrMagnitude <= Threshold)
        {
            if (!_isAudioPlayed && BlackBoard.AudioSource != null)
            {
                BlackBoard.AudioSource.Play();
                _isAudioPlayed = true;
            }

            // ���̉�
            Color validColor = _baseColor;
            validColor.a = 1;
            BlackBoard.PoiSprite.color = validColor;

            // �v���C���[�����m������グ��
            Collider2D player = Physics2D.OverlapCircle(BlackBoard.transform.position, BlackBoard.DetectRadius, BlackBoard.PlayerLayer);
            if (player != null)
            {
                // �q�b�g��ԂɑJ��
                TryChangeState(BlackBoard[PoiStateType.Hit]);
                return;
            }

            // ���΂炭�҂�
            _timer += Time.deltaTime;
            if (_timer > Delay)
            {
                // ��������
                BlackBoard.PoiSprite.color = _baseColor;
                // �߂��ԂɑJ��
                TryChangeState(BlackBoard[PoiStateType.MoveBack]);
            }
        }
        // ������
        else
        {
            // �ړ�
            BlackBoard.transform.Translate(dirVector.normalized * Time.deltaTime * _currentSpeed);
            _currentSpeed *= 1.005f;
        }
    }
}