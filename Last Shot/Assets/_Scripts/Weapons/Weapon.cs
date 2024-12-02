using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject projectile;

    [SerializeField] private int ammo;
    [SerializeField] private int maxAmmo;

    [SerializeField] private int numOfProjectiles;

    [SerializeField] private float fireRate;
    private Timer cooldown;

    private bool triggerPulled;
    private void Start()
    {
        Input.instance.Shoot.perform += TryShoot;
        Input.instance.Shoot.cancel += StopShoot;

        cooldown = new Timer(fireRate);
    }

    private void TryShoot()
    {
        triggerPulled = true;
        if (ammo > 0 && cooldown.Ready)
        {
            StartCoroutine(nameof(TriggerPulled));
        }
    }

    IEnumerator TriggerPulled()
    {
        while (triggerPulled && ammo > 0)
        {
            Shoot();
            yield return new WaitUntil(()=>cooldown.Ready);
        }
    }

    private void Shoot()
    {
        StartCoroutine(cooldown.StartTimer());
        for (int i = 0; i < numOfProjectiles; i++)
        {
            var obj = PoolManager.SpawnObject(projectile, muzzle.position, muzzle.rotation).GetComponent<Projectile>();
                    obj.Initialize();
        }
        

        ammo--;
    }

    private void StopShoot()
    {
        triggerPulled = false;
        StopCoroutine(TriggerPulled());
    }
    
}
