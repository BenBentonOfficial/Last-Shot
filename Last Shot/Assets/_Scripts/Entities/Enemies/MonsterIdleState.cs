using UnityEngine;

public class MonsterIdleState : MonsterState
{
    public MonsterIdleState(EMonsterState key, Monster monster) : base(key, monster)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        _monster.ZeroVelocity();
    }

    public override EMonsterState GetNextState()
    {
        if (_monster.Health <= 0)
            return EMonsterState.Death;

        if (_monster.PlayerInRange())
        {
            return EMonsterState.Move;
        }
        
        return base.GetNextState();
    }
}
