using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum DropType
{
    experience,
    health
}
public class Pickup : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private bool pickedUp = false;
    private Vector2 directionToPlayer;

    [SerializeField] private DropType dropType;

    [SerializeField] private int dropValue = 1;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered");
        directionToPlayer = (col.transform.position - transform.position).normalized;
        _rigidbody2D.AddForce(-directionToPlayer * 10, ForceMode2D.Impulse);
        Invoke("Grab", 0.25f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            switch (dropType)
            {
                case DropType.experience:
                    LevelManager.GainExperience(dropValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            PoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void Grab()
    {
        pickedUp = true;
    }

    private void FixedUpdate()
    {
        if(!pickedUp)
            return;
        
        var dir = (Input.instance.PlayerPos() - transform.position).normalized;
        _rigidbody2D.linearVelocity  = (dir * 7);
    }
}