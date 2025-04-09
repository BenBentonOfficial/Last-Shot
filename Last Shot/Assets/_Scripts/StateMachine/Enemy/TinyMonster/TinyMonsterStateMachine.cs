public class TinyMonsterStateMachine : MonsterStateMachine
{
    public void Initialize(Monster monster)
    {
        States.Add(EMonsterState.Idle, new MonsterIdleState(EMonsterState.Idle, monster));
        States.Add(EMonsterState.Move, new MonsterMoveState(EMonsterState.Move, monster));
        States.Add(EMonsterState.Death, new MonsterDeathState(EMonsterState.Death, monster));
        States.Add(EMonsterState.Attack, new MonsterAttackState(EMonsterState.Attack, monster));
        

        CurrentState = States[EMonsterState.Idle];
    }
}
