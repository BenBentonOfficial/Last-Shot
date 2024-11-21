
public class PlayerIdleState : PlayerState
{

    public PlayerIdleState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.ZeroVelocity();
    }


    public override void UpdateState()
    {
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Input.MovementInput().magnitude > 0)
            return PlayerStateMachine.EPlayerState.Move;
        
        return StateKey;
    }

    public override void AnimEndTrigger()
    {
        base.AnimEndTrigger();
        
    }
}
