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
    /// Enter()が呼ばれてかつ、ステートの遷移処理を呼んでいない場合のみ遷移可能
    /// </summary>
    public bool TryChangeState(PoiStateBase nextState)
    {
        if (_stage == Stage.Enter)
        {
            Debug.LogWarning("Enter()が呼ばれる前にステートを遷移することは不可能: 遷移先: " + nextState);
            return false;
        }
        else if (_stage == Stage.Exit)
        {
            Debug.LogWarning("既に別のステートに遷移する処理が呼ばれています: 遷移先: " + nextState);
            return false;
        }

        _stage = Stage.Exit;
        _nextState = nextState;

        return true;
    }
}