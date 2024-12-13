using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [NonSerialized] public PlayerStateMachine _stateMachine;
    
    public PlayerData _playerData;
    [SerializeField] private Slider rollCooldownSlider;
    
    protected override void Awake()
    {
        base.Awake();

        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb, _anim);

        _playerData.DashTimer = new Timer(_playerData.dashCooldown, rollCooldownSlider);
    }
    
}

[CreateAssetMenu(fileName = "New Player", menuName = "New Player")]
public class PlayerData : ScriptableObject
{
    public float dashSpeed;
    public float dashCooldown;

    public Timer DashTimer;
}
