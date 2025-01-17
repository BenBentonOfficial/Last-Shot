using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    protected Rigidbody2D _rb;
    protected Animator _anim;

    #region Health
    
    private float currentHealth;
    private float maxHealth;

    public float Health
    {
        get => currentHealth; 
        set => currentHealth = value;
    }
    public float HealthP => currentHealth / maxHealth;
    public void Damage(float damage, DamageType type)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }
    
    #endregion

    [SerializeField] protected float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    public Animator Anim => _anim;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetAnimState(string state ,bool value)
    {
        _anim.SetBool(state, value);
    }

    public void ZeroVelocity() => _rb.linearVelocity = Vector2.zero;
    public void SetVelocity(Vector2 newVelocity) => _rb.linearVelocity = newVelocity;
    public Vector2 Velocity => _rb.linearVelocity;

}
