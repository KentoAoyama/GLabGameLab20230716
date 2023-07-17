using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーにヒットした際に送信されるメッセージの構造体
/// </summary>
public struct HitMessage { }

/// <summary>
/// プレイヤーにヒットした状態
/// </summary>
public class HitState : PoiStateBase
{
    public HitState(PoiStateType type, PoiBlackBoard blackBoard) : base(type, blackBoard) { }

    protected override void Enter()
    {
        // プレイヤーにヒットしたメッセージを送信
        MessageBroker.Default.Publish(new HitMessage());
    }

    protected override void Exit()
    {
    }

    protected override void Stay()
    {
        Debug.Log("ぷれやーヒット");
    }
}
