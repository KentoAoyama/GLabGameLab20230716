using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初期化 1フレームで次の状態へ
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
