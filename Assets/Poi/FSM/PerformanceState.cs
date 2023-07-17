using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ‰‰o ‚Ñ‚Á‚­‚è‰‰oŒã‚ÉŸ‚Ìó‘Ô‚Ö
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