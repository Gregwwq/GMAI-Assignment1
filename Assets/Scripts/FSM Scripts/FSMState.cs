using UnityEngine;
using System.Collections;

public abstract class FSMState<T>
{
    // using readonly so the name cannot be changed and can be initialized at runtime
    public readonly T Name;

    protected FSM<string> fsm;

    public FSMState(FSM<string> _fsm, T _name)
    {
        fsm = _fsm;
        Name = _name;
    }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}

