using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform muzzle;
    
    private int ammo;

    private Timer cooldown;

    private bool triggerPulled;

    #region Test Params

    public float spreadAngle;
    public float projectileSpeed;

    #endregion
    
    private void Start()
    {
        Input.instance.Shoot.perform += TryShoot;
        Input.instance.Shoot.cancel += StopShoot;

        ammo = _weaponData.maxAmmo;

        cooldown = new Timer(_weaponData.fireRate);
    }

    public void TryShoot()
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

        float halfSpread = spreadAngle / 2f;
        float angleStep = spreadAngle / (_weaponData.numOfProjectiles - 1);
        
        for (int i = 0; i < _weaponData.numOfProjectiles; i++)
        {
            var angle = -halfSpread + angleStep * i;
            var direction = Quaternion.Euler(0, 0, angle) * transform.right;
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            var obj = PoolManager.SpawnObject(_weaponData.projectile, muzzle.position, Quaternion.Euler(0,0,angleToFace)).GetComponent<Projectile>();
            obj.Initialize(direction, projectileSpeed);
        }
        
        ammo--;
    }

    private void StopShoot()
    {
        triggerPulled = false;
        StopCoroutine(TriggerPulled());
    }
    
}
