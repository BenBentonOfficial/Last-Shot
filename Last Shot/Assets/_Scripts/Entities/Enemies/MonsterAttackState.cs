using UnityEngine;

public class MonsterAttackState : MonsterState
{
    public MonsterAttackState(EMonsterState key, Monster monster) : base(key, monster)
    {
    }


    public override EMonsterState GetNextState()
    {
        if (_monster.Health <= 0)
            return EMonsterState.Death;
        
        if (animEnded)
            return EMonsterState.Idle;

        
        
        return base.GetNextState();
    }
}
