using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ 1�t���[���Ŏ��̏�Ԃ�
/// </summary>
public class InitState : PoiStateBase
{
    public InitState(PoiStateType type, PoiBlackBoard blackBoard) : base(type, blackBoard) { }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }

    protected override void Stay()
    {
        TryChangeState(BlackBoard[PoiStateType.MoveForward]);
    }
}
