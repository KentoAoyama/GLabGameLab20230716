using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PoiBlackBoard : MonoBehaviour
{
    [Header("ポイ")]
    [SerializeField] SpriteRenderer _poiSprite;
    [Header("ポイの判定の中心")]
    [SerializeField] Transform _pivot;
    [Header("ポイを引き上げる半径")]
    [Tooltip("ポイ静止時にこの範囲に入った場合は、引き上げの演出を行いヒット判定になる")]
    [SerializeField] float _detectRadius;
    [Header("プレイヤーの属するレイヤー")]
    [SerializeField] LayerMask _playerLayer;

    AudioSource _audioSource;
    Dictionary<PoiStateType, PoiStateBase> _stateDict;
    Vector2? _targetPos;
    Vector2? _initPos;

    public AudioSource AudioSource => _audioSource;
    public SpriteRenderer PoiSprite => _poiSprite;
    public PoiStateBase this[PoiStateType key]
    {
        get
        {
            if (_stateDict.TryGetValue(key, out PoiStateBase value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException("対応する状態が無い: " + key);
            }
        }
    }
    public Vector2 PivotPos => _pivot.position;
    public float DetectRadius => _detectRadius;
    public LayerMask PlayerLayer => _playerLayer;

    public Vector2 TargetPos
    {
        get
        {
            if(_targetPos == null)
            {
                throw new NullReferenceException("ターゲットの位置がナル");
            }
            else
            {
                return (Vector2)_targetPos;
            }
        }
        set => _targetPos = value;
    }
    public Vector2 InitPos
    {
        get
        {
            if (_initPos == null)
            {
                throw new NullReferenceException("初期位置がナル");
            }
            else
            {
                return (Vector2)_initPos;
            }
        }
        set => _initPos = value;
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        CreateState();
    }

    void CreateState()
    {
        _stateDict = new(4);
        _stateDict.Add(PoiStateType.Init, new InitState(PoiStateType.Init, this));
        _stateDict.Add(PoiStateType.MoveForward, new MoveForwardState(PoiStateType.MoveForward, this));
        _stateDict.Add(PoiStateType.MoveBack, new MoveBackState(PoiStateType.MoveBack, this));
        _stateDict.Add(PoiStateType.Hit, new HitState(PoiStateType.Hit, this));
    }

    void OnDrawGizmos()
    {
        if (_pivot != null)
        {
            // ポイを引き上げる範囲
            Gizmos.DrawWireSphere(PivotPos, _detectRadius);
        }
    }
}
