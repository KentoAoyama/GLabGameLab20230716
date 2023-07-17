using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ‰Šú‰» 1ƒtƒŒ[ƒ€‚ÅŸ‚Ìó‘Ô‚Ö
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
