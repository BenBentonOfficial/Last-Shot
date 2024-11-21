
using UnityEngine;

public class Player : Entity
{
    public PlayerStateMachine _stateMachine;

    [SerializeField] private float dashSpeed;
    public float DashSpeed => dashSpeed;
    [SerializeField] private float dashLength;
    public float DashLength => dashLength;
    [SerializeField] private float dashCooldown;
    public float DashCooldown => dashCooldown;
    
    protected override void Awake()
    {
        base.Awake();

        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb, _anim);
    }
    
}
