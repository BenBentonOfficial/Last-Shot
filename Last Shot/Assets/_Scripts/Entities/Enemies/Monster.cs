using UnityEngine;

public class Monster : Entity
{
    private Transform player;

    [SerializeField] private float range;
    
    [SerializeField] private GameObject floatingDamageText;

    [SerializeField] private GameObject experienceDrop;

    protected override void Awake()
    {
        base.Awake();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Damage(float damage, DamageType type)
    {
        var text = PoolManager.SpawnObject(floatingDamageText, transform.position, Quaternion.identity);
        text.GetComponent<FloatingDamageText>().Initialize(damage, type);
        
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
        }
    }

    public void Death()
    {
        PoolManager.SpawnObject(experienceDrop, transform.position, Quaternion.identity);
        PoolManager.ReturnObjectToPool(gameObject);
    }

    public bool PlayerInRange() => Vector3.Distance(transform.position, player.position) <= range;
    public Vector2 DirectionToPlayer() => (player.position - transform.position).normalized;
}
