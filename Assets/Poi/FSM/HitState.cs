using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �v���C���[�Ƀq�b�g�����ۂɑ��M����郁�b�Z�[�W�̍\����
/// </summary>
public struct HitMessage { }

/// <summary>
/// �v���C���[�Ƀq�b�g�������
/// </summary>
public class HitState : PoiStateBase
{
    public HitState(PoiStateType type, PoiBlackBoard blackBoard) : base(type, blackBoard) { }

    protected override void Enter()
    {
        // �v���C���[�Ƀq�b�g�������b�Z�[�W�𑗐M
        MessageBroker.Default.Publish(new HitMessage());
    }

    protected override void Exit()
    {
    }

    protected override void Stay()
    {
        Debug.Log("�Ղ��[�q�b�g");
    }
}
