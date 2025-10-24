using System;
using UnityEngine;

public class Player : Entity
{
    [NonSerialized] public PlayerStateMachine _stateMachine;

    public PlayerData _playerData;
    PlayerUI PlayerUI;
    
    //Item test area
    public ItemCollection _itemCollection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemPickup item))
        {
            _itemCollection.EquipItem(item.item, this);
        }
    }


    protected override void Awake()
    {
        base.Awake();

        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb, _anim);
        
        PlayerUI = GetComponent<PlayerUI>();

        _playerData.DashTimer = new Timer(_playerData.dashCooldown, PlayerUI.rollCooldownSlider);
    }
}

[CreateAssetMenu(fileName = "New Player", menuName = "New Player")]
public class PlayerData : ScriptableObject
{
    public float dashSpeed;
    public float dashCooldown;

    public Timer DashTimer;
}
