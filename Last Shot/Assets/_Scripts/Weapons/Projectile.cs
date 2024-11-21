using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float lifetime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        rb.velocity = transform.right * 10;
        StartCoroutine(nameof(LifeCycle));

    }

    IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifetime);
        PoolManager.ReturnObjectToPool(gameObject);
    }

}
