public class MonsterState : State<EMonsterState>
{
    public MonsterState(EMonsterState key, Monster monster) : base(key)
    {
        _monster = monster;
    }

    protected Monster _monster;

    public override void EnterState()
    {
        base.EnterState();
        _monster.SetAnimState(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        base.ExitState();
        _monster.SetAnimState(StateKey.ToString(), true);
    }


    public override EMonsterState GetNextState()
    {
        return StateKey;
    }
}
