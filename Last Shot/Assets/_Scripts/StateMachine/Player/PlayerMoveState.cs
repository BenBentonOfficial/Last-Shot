
public class PlayerMoveState : PlayerState
{


    public PlayerMoveState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        player.SetVelocity(Input.MovementInput() * player.MoveSpeed);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Input.MovementInput().magnitude <= 0)
            return PlayerStateMachine.EPlayerState.Idle;

        if (Input.instance.Roll.Queued)
        {
            return PlayerStateMachine.EPlayerState.Roll;
        }
        
        return StateKey;
    }
}
