
public class PlayerRollState : PlayerState
{

    public PlayerRollState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        player.SetVelocity(Input.MovementInput() * player.DashSpeed);

    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }
        return StateKey;
    }


}
