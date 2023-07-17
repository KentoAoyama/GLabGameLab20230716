using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 前進 到着後しばらく待機して次の状態へ
/// </summary>
public class MoveForwardState : PoiStateBase
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
    const float Delay = 0.5f;

    Color _baseColor;
    float _currentSpeed;
    /// <summary>
    /// 到着後の待機時間をカウントする
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
        // 到着
        if(dirVector.sqrMagnitude <= Threshold)
        {
            if (!_isAudioPlayed && BlackBoard.AudioSource != null)
            {
                BlackBoard.AudioSource.Play();
                _isAudioPlayed = true;
            }

            // 実体化
            Color validColor = _baseColor;
            validColor.a = 1;
            BlackBoard.PoiSprite.color = validColor;

            // プレイヤーを検知したら上げる
            Collider2D player = Physics2D.OverlapCircle(BlackBoard.transform.position, BlackBoard.DetectRadius, BlackBoard.PlayerLayer);
            if (player != null)
            {
                // ヒット状態に遷移
                TryChangeState(BlackBoard[PoiStateType.Hit]);
                return;
            }

            // しばらく待つ
            _timer += Time.deltaTime;
            if (_timer > Delay)
            {
                // 半透明化
                BlackBoard.PoiSprite.color = _baseColor;
                // 戻る状態に遷移
                TryChangeState(BlackBoard[PoiStateType.MoveBack]);
            }
        }
        // 未到着
        else
        {
            // 移動
            BlackBoard.transform.Translate(dirVector.normalized * Time.deltaTime * _currentSpeed);
            _currentSpeed *= 1.005f;
        }
    }
}