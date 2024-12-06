
public class PlayerRollState : PlayerState
{

    public PlayerRollState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }
    
    public override void EnterState()
    {
        base.EnterState();
        player.SetVelocity(Input.MovementInput() * (player._playerData.dashSpeed + player.MoveSpeed));
        player._playerData.dashSpeed = player._playerData.dashSpeed;
        
    }

    public override void ExitState()
    {
        base.ExitState();
        player.StartCoroutine(player._playerData.DashTimer.StartTimer());
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
