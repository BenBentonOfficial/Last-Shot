using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Item", menuName = "New Item / Ability Item")]
public class AbilityItem : Item
{
    public PlayerStateMachine.EPlayerState affectedPlayerState;
    public bool onEnter;
    public override void Initialize(Player player)
    {
        if(onEnter) ApplyEffect(ref player._stateMachine.GetState(affectedPlayerState).enterState);
        else ApplyEffect(ref player._stateMachine.GetState(affectedPlayerState).exitState);
    }

    public override float Value()
    {
        throw new NotImplementedException();
    }

    public void ApplyEffect(ref Action action)
    {
        action += ActiveEffect;
    }

    public void ActiveEffect()
    {
        Debug.Log("Boooom!");
    }
}
