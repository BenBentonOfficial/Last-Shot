using UnityEngine;

public enum EMonsterState
{
    Idle,
    Move,
    Attack,
    Hurt,
    Death
}

public class MonsterStateMachine : StateMachine<EMonsterState>
{
    //TODO: Add initializer (Check Player State Machine)
}
