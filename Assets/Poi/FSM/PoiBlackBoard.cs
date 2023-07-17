using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PoiBlackBoard : MonoBehaviour
{
    [Header("�|�C")]
    [SerializeField] SpriteRenderer _poiSprite;
    [Header("�|�C�̔���̒��S")]
    [SerializeField] Transform _pivot;
    [Header("�|�C�������グ�锼�a")]
    [Tooltip("�|�C�Î~���ɂ��͈̔͂ɓ������ꍇ�́A�����グ�̉��o���s���q�b�g����ɂȂ�")]
    [SerializeField] float _detectRadius;
    [Header("�v���C���[�̑����郌�C���[")]
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
                throw new KeyNotFoundException("�Ή������Ԃ�����: " + key);
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
                throw new NullReferenceException("�^�[�Q�b�g�̈ʒu���i��");
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
                throw new NullReferenceException("�����ʒu���i��");
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
            // �|�C�������グ��͈�
            Gizmos.DrawWireSphere(PivotPos, _detectRadius);
        }
    }
}
