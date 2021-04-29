using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildState))]
[RequireComponent(typeof(WaveState))]
public class GameplayStateMachine : MonoBehaviour
{
    public enum GameplayStateType
    {
        BuildState,
        WaveState
    }

    private Dictionary<GameplayStateType, GameplayState> _stateMap;
    private GameplayStateType _currentState;

    private void Awake()
    {
        SetupStatemachine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState(GameplayStateType.WaveState);
        }
        _stateMap[_currentState].StateUpdate(Time.deltaTime);
    }

    // called from UI, int argument for now
    // so we can pass an int via unityevent
    public void SetState(int enumNumber)
    {
        SetState((GameplayStateType)enumNumber);
    }
    
    public void SetState(GameplayStateType newState)
    {
        _stateMap[_currentState].StateExit();
        _currentState = newState;
        _stateMap[_currentState].StateEnter();
    }

    private void SetupStatemachine()
    {
        BuildState buildState = GetComponent<BuildState>();
        WaveState waveState = GetComponent<WaveState>();
        
        _stateMap = new Dictionary<GameplayStateType, GameplayState>();
        _stateMap.Add(GameplayStateType.BuildState, buildState);
        _stateMap.Add(GameplayStateType.WaveState, waveState);
        _currentState = GameplayStateType.BuildState;
        
        waveState.StateExit();
        buildState.StateEnter();

    }
}
