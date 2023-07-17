using UnityEngine;

public enum PoiStateType
{
    Init,
    MoveForward,
    MoveBack,
    Hit,
}

public abstract class PoiStateBase
{
    enum Stage
    {
        Enter,
        Stay,
        Exit,
    }

    Stage _stage;
    PoiStateBase _nextState;
    PoiBlackBoard _blackBoard;

    public PoiStateBase(PoiStateType type, PoiBlackBoard blackBoard)
    {
        Type = type;
        _blackBoard = blackBoard;
    }

    public PoiBlackBoard BlackBoard => _blackBoard;
    public PoiStateType Type { get; }

    public PoiStateBase Update()
    {
        if (_stage == Stage.Enter)
        {
            Enter();
            _stage = Stage.Stay;
        }
        else if (_stage == Stage.Stay)
        {
            Stay();
        }
        else if (_stage == Stage.Exit)
        {
            Exit();
            _stage = Stage.Enter;

            return _nextState;
        }

        return this;
    }

    protected abstract void Enter();
    protected abstract void Stay();
    protected abstract void Exit();
    protected virtual void OnInvalid() { }

    /// <summary>
    /// Enter()���Ă΂�Ă��A�X�e�[�g�̑J�ڏ������Ă�ł��Ȃ��ꍇ�̂ݑJ�ډ\
    /// </summary>
    public bool TryChangeState(PoiStateBase nextState)
    {
        if (_stage == Stage.Enter)
        {
            Debug.LogWarning("Enter()���Ă΂��O�ɃX�e�[�g��J�ڂ��邱�Ƃ͕s�\: �J�ڐ�: " + nextState);
            return false;
        }
        else if (_stage == Stage.Exit)
        {
            Debug.LogWarning("���ɕʂ̃X�e�[�g�ɑJ�ڂ��鏈�����Ă΂�Ă��܂�: �J�ڐ�: " + nextState);
            return false;
        }

        _stage = Stage.Exit;
        _nextState = nextState;

        return true;
    }
}