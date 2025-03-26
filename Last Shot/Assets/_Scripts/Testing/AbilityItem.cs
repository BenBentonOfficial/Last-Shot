using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Item", menuName = "New Item / Ability Item")]
public class AbilityItem : Item
{
    public PlayerStateMachine.EPlayerState affectedPlayerState;
    public bool onEnter;
    
    public override void Equip(Player player)
    {
        if(onEnter) Subscribe(ref player._stateMachine.GetState(affectedPlayerState).enterState);
        else Subscribe(ref player._stateMachine.GetState(affectedPlayerState).exitState);
    }

    public override void Unequip(Player player)
    {
        if(onEnter) Unsubscribe(ref player._stateMachine.GetState(affectedPlayerState).enterState);
        else Unsubscribe(ref player._stateMachine.GetState(affectedPlayerState).exitState);
    }

    public override float Value()
    {
        throw new NotImplementedException();
    }

    private void Subscribe(ref Action action)
    {
        action += ActiveEffect;
    }

    private void Unsubscribe(ref Action action)
    {
        action -= ActiveEffect;
    }

    public void ActiveEffect()
    {
        Debug.Log("Boooom!");
    }
}
