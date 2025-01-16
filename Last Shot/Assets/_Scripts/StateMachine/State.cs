using System;
using UnityEngine;

public abstract class State<EState> where EState : Enum
{
    protected State(EState key)
    {
        StateKey = key;
    }
    
    public EState StateKey { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    protected float stateTimer;
    protected bool animEnded;

    public Action enterState;
    public Action exitState;

    public virtual void EnterState()
    {
        animEnded = false;
        enterState?.Invoke();
    }

    public virtual void ExitState()
    {
        exitState?.Invoke();
    }

    public virtual void UpdateState()
    {
        stateTimer -= Time.deltaTime;
    }
    public abstract EState GetNextState();

    public virtual void AnimEndTrigger()
    {
        animEnded = true;
    }
}
