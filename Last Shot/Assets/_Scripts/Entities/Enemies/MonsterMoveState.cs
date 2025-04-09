public class MonsterMoveState : MonsterState
{
    public MonsterMoveState(EMonsterState key, Monster monster) : base(key, monster)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        _monster.SetVelocity(_monster.DirectionToPlayer() * _monster.MoveSpeed);
    }

    public override EMonsterState GetNextState()
    {
        if (_monster.Health <= 0)
            return EMonsterState.Death;
        
        if(!_monster.PlayerInRange())
            return EMonsterState.Idle;
        
        return base.GetNextState();
    }
}
