using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle, 
        Move,
        Roll
    }

    public void Initialize(Player player, Rigidbody2D rb, Animator anim)
    {
        States.Add(EPlayerState.Idle, new PlayerIdleState(EPlayerState.Idle, player));
        States.Add(EPlayerState.Move, new PlayerMoveState(EPlayerState.Move, player));
        States.Add(EPlayerState.Roll, new PlayerRollState(EPlayerState.Roll, player));

        CurrentState = States[EPlayerState.Idle];
    }
}
