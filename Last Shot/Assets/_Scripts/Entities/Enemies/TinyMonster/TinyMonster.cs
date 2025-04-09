using System;
public class TinyMonster : Monster
{
    [NonSerialized] public TinyMonsterStateMachine stateMachine;
    
    protected override void Awake()
    {
        base.Awake();

        stateMachine = GetComponent<TinyMonsterStateMachine>();
        stateMachine.Initialize(this);
    }
}
