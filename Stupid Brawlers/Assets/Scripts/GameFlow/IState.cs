public interface IState : IStateExit
{
    void Enter();
}

public interface IParameterizedState : IStateExit
{
    void Enter(string parameter);
}

public interface IStateExit
{
    void Exit();
}
