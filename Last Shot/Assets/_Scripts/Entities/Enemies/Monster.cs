using UnityEngine;

public class Monster : Entity
{
    private Transform player;

    [SerializeField] private float range;
    
    [SerializeField] private GameObject floatingDamageText;

    protected override void Awake()
    {
        base.Awake();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Damage(float damage, DamageType type)
    {
        base.Damage(damage, type);
        
        var text = PoolManager.SpawnObject(floatingDamageText, transform.position, Quaternion.identity);
        text.GetComponent<FloatingDamageText>().Initialize(damage, type);
    }

    public bool PlayerInRange() => Vector3.Distance(transform.position, player.position) <= range;
    public Vector2 DirectionToPlayer() => (player.position - transform.position).normalized;
}
