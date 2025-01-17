using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly GameStateFactory _gameStateFactory;
    
    private IState _currentState;
    private IParameterizedState _currentParameterizedState;
    
    private readonly Dictionary<Type, IStateExit> _states;

    public GameStateMachine(GameStateFactory gameStateFactory)
    {
        _gameStateFactory = gameStateFactory;
        _gameStateFactory.SetGameStateMachine(this);

        _states = new Dictionary<Type, IStateExit>()
        {
            [typeof(InitState)] =  _gameStateFactory.CreateInitState(),             
            [typeof(LoadMenuState)] =  _gameStateFactory.CreateLoadMenuState(),
            [typeof(LoadLevelState)] =  _gameStateFactory.CreateLoadLevelState(),
        };
    }
   
    public void Enter<TState>()
    {
        _currentState?.Exit();
        _currentParameterizedState?.Exit();
        
        _currentState = null;
        _currentParameterizedState = null;
        
        _currentState = _states[typeof(TState)] as IState;
        _currentState?.Enter();
    }
    
    public void Enter<TState>(string argument) where TState : IParameterizedState
    {
        _currentState?.Exit();
        _currentParameterizedState?.Exit();
        
        _currentState = null;
        _currentParameterizedState = null;

        if (!_states.TryGetValue(typeof(TState), out var state))
            throw new InvalidOperationException($"Состояние типа {typeof(TState)} не найдено.");
    
        _currentParameterizedState = state as IParameterizedState;
        _currentParameterizedState?.Enter(argument);
    }
    
    public void Run()
    {
        Enter<InitState>();
    }

    public void RunToLaboratoryMode() => Enter<LoadLevelState>("Laboratory");

    public void Exit()
    {
      
    }
}