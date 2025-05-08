using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float lifetime;

    private float _damage;
    private DamageType _damageType;

    private int _peirce;
    private int _peirceCount;

    private Transform _unhittable;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed, float damage, int peirce, DamageType damageType)
    {
        rb.linearVelocity = direction * speed;
        _damage = damage;
        _damageType = damageType;
        _peirce = peirce;
        _peirceCount = 0;
        StartCoroutine(nameof(LifeCycle));

    }

    public void Initialize(Vector2 direction, float speed, float damage, int peirce, Transform unhittable)
    {
        rb.linearVelocity = direction * speed;
        _damage = damage;
        _peirce = peirce;
        _peirceCount = 0;
        _unhittable = unhittable;
        StartCoroutine(nameof(LifeCycle));
    }

    IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifetime);
        PoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _unhittable)
            return;
        
        if (other.TryGetComponent(out IDamageable health))
        {
        
            health.Damage(_damage,_damageType);
            foreach (var behavior in GetComponents<BulletBehaviour>())
            {
                behavior.OnHit(other.gameObject, transform.position);
            }
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
