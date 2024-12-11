using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestDummy : MonoBehaviour, IDamageable
{
    private float health = 1000;
    private float maxHealth = 1000;
    public float Health { get => health; set => health = value; }

    public float regenRate;
    public float regenAmount;
    public float regenWaitTime;
    public float timeSinceLastHit;
    public bool canRegen => health < maxHealth && Time.time > timeSinceLastHit + regenWaitTime;
    public bool isRegen = false;

    public Slider healthSlider;

    public GameObject floatingDamageText;
    
    public void Damage(float damage, DamageType type)
    {
        isRegen = false;
        StopCoroutine("Regen");
        timeSinceLastHit = Time.time;
        health -= damage;
        
        PoolManager.SpawnObject(floatingDamageText, transform.position, Quaternion.identity).GetComponent<FloatingDamageText>().Initialize(damage, type);
    }

    private void Update()
    {
        if (canRegen && !isRegen)
        {
            StartCoroutine("Regen");
        }
        
        healthSlider.value = health / maxHealth;
    }

    private IEnumerator Regen()
    {
        isRegen = true;
        while (canRegen)
        {
            health += regenAmount;
            yield return new WaitForSeconds(1f / regenRate);
        }
    }
}
