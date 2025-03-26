public class TinyMonsterStateMachine : MonsterStateMachine
{
    public void Initialize(Monster monster)
    {
        States.Add(EMonsterState.Idle, new MonsterIdleState(EMonsterState.Idle,monster));
        States.Add(EMonsterState.Move, new MonsterMoveState(EMonsterState.Move,monster));

        CurrentState = States[EMonsterState.Idle];
    }
}
