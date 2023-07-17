using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戻り 初期位置に到達後 メッセージ送信
/// </summary>
public class MoveBackState : PoiStateBase
{
    /// <summary>
    /// 位置がほぼ等しいを判定する用
    /// </summary>
    const float Threshold = 0.005f;
    /// <summary>
    /// 移動速度
    /// </summary>
    const float Speed = 6.0f;
    /// <summary>
    /// 到着後の待機時間
    /// </summary>
    const float Delay = 1.0f;

    /// <summary>
    /// 到着後の待機時間をカウントする
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
        // 到着
        if (dirVector.sqrMagnitude <= Threshold)
        {
            // しばらく待つ
            _timer += Time.deltaTime;
            if (_timer > Delay)
            {
                Debug.Log("おわり");
                BlackBoard.gameObject.SetActive(false);
            }
        }
        // 未到着
        else
        {
            // 移動
            BlackBoard.transform.Translate(dirVector.normalized * Time.deltaTime * Speed);
        }
    }
}