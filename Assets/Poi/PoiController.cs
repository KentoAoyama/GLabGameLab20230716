using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

/// <summary>
/// ���� �������ꂽ�^�C�~���O�ŋN��
/// �v���C���[����ʏ�ɂ���O��
/// </summary>
[RequireComponent(typeof(PoiBlackBoard))]
public class PoiController : MonoBehaviour
{
    [Header("���o�p�т�����")]
    [SerializeField] GameObject _icon;
    [Header("�|�C�{��")]
    [SerializeField] Transform _poiSprite;
    [Header("�v���C���[�擾�p���")]
    [SerializeField] string _playerTag;

    PoiBlackBoard _blackBoard;
    PoiStateBase _currentState;

    void Awake()
    {
        // ����p����������
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
    /// �^�O�Ńv���C���[���擾���č��ɏ�������
    /// �߂��Ă��鎞�p�Ɍ��ݒn�����ɏ�������
    /// </summary>
    Vector3 WriteTargetPosOnBlackBoard()
    {
        GameObject player = GameObject.FindGameObjectWithTag(_playerTag);
        if(player == null)
        {
            throw new NullReferenceException("�^�O�Ńv���C���[���擾���s: " + _playerTag);
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
    /// �т����艉�o
    /// </summary>
    public IEnumerator PerformanceCoroutine()
    {
        _icon.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _icon.SetActive(false);
    }
}