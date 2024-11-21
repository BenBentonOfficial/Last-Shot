using System;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationEndTrigger()
    {
        player._stateMachine.GetCurrentState().AnimEndTrigger();
    }
}
