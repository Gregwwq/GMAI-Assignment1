using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM<T>
{
    Dictionary<T, FSMState<T>> states = new Dictionary<T, FSMState<T>>();
    FSMState<T> currentState = null;

    public FSM()
    {

    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void AddState(FSMState<T> state)
    {
        if (state != null)
        {
            states.Add(state.Name, state);
        }
    }
    public void AddState(FSMState<T> state, T name)
    {
        if (state != null)
        {
            states.Add(name, state);
        }
    }

    public void SetState(T name)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = states[name];
        currentState.Enter();
    }
    public void SetState(FSMState<T> nextState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = nextState;
        currentState.Enter();
    }
}