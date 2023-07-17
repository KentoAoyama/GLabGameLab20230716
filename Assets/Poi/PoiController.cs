using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

/// <summary>
/// 制御 生成されたタイミングで起動
/// プレイヤーが画面上にいる前提
/// </summary>
[RequireComponent(typeof(PoiBlackBoard))]
public class PoiController : MonoBehaviour
{
    [Header("演出用びっくり")]
    [SerializeField] GameObject _icon;
    [Header("ポイ本体")]
    [SerializeField] Transform _poiSprite;
    [Header("プレイヤー取得用情報")]
    [SerializeField] string _playerTag;

    PoiBlackBoard _blackBoard;
    PoiStateBase _currentState;

    void Awake()
    {
        // 黒板を用いた初期化
        _blackBoard = GetComponent<PoiBlackBoard>();
        Vector3 targetPos = WriteTargetPosOnBlackBoard();
        RotatePoiSprite(targetPos);
        _currentState = _blackBoard[PoiStateType.Init];
    }

    IEnumerator Start()
    {
        yield return PerformanceCoroutine();
        this.UpdateAsObservable().Subscribe(_ => _currentState = _currentState.Update());
    }

    /// <summary>
    /// タグでプレイヤーを取得して黒板に書き込む
    /// 戻ってくる時用に現在地も黒板に書き込む
    /// </summary>
    Vector3 WriteTargetPosOnBlackBoard()
    {
        GameObject player = GameObject.FindGameObjectWithTag(_playerTag);
        if(player == null)
        {
            throw new NullReferenceException("タグでプレイヤーを取得失敗: " + _playerTag);
        }

        _blackBoard.TargetPos = player.transform.position;
        _blackBoard.InitPos = transform.position;

        return _blackBoard.TargetPos;
    }

    void RotatePoiSprite(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        _poiSprite.up = dir;
    }

    /// <summary>
    /// びっくり演出
    /// </summary>
    public IEnumerator PerformanceCoroutine()
    {
        _icon.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _icon.SetActive(false);
    }
}