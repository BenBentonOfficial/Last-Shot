public interface IDamageable
{
    
    public float Health { get; set; }
    public void Damage(float damage, DamageType type);
    
}
