using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���o �т����艉�o��Ɏ��̏�Ԃ�
/// </summary>
public class PerformanceState : PoiStateBase
{
    public PerformanceState(PoiStateType type, PoiBlackBoard blackBoard) : base(type, blackBoard) { }

    bool _completed;

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }

    protected override void Stay()
    {
        if (_completed)
        {
            TryChangeState(BlackBoard[PoiStateType.MoveForward]);
        }
    }
}