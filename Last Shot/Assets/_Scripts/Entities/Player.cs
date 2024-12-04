
using UnityEngine;

public class Player : Entity
{
    public PlayerStateMachine _stateMachine;
    
    public PlayerData _playerData;
    
    protected override void Awake()
    {
        base.Awake();

        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb, _anim);

        _playerData.DashTimer = new Timer(_playerData.dashCooldown);
    }
    
}

[CreateAssetMenu(fileName = "New Player", menuName = "New Player")]
public class PlayerData : ScriptableObject
{
    public float dashSpeed;
    public float dashCooldown;

    public Timer DashTimer;
}
