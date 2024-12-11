using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float lifetime;

    private float _damage;
    private DamageType _damageType;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed, float damage, DamageType damageType)
    {
        rb.linearVelocity = direction * speed;
        _damage = damage;
        _damageType = damageType;
        StartCoroutine(nameof(LifeCycle));

    }

    IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifetime);
        PoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamageable>()?.Damage(_damage, _damageType);
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
