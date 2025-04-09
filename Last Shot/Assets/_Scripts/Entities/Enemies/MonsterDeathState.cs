public class MonsterDeathState : MonsterState
{
    public MonsterDeathState(EMonsterState key, Monster monster) : base(key, monster)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        _monster.ZeroVelocity();
    }
}
