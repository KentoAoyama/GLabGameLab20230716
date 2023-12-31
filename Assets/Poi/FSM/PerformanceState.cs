using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 演出 びっくり演出後に次の状態へ
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