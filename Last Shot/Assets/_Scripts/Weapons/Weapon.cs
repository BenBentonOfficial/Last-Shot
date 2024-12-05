using System.Collections;
using UnityEngine;
using UnityEngine.UI;

enum WeaponState
{
    IDLE,
    FIRE,
    RELOAD
}
public class Weapon : MonoBehaviour
{
    private WeaponState state;
    
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform muzzle;
    
    private int ammo;

    private Timer cooldown;

    private bool triggerPulled;

    #region Test Params

    public float spreadAngle;
    public float projectileSpeed;

    #endregion

    [SerializeField] private GameObject ReloadUI;
    [SerializeField] private Slider reloadSlider;

    private void Start()
    {
        Input.instance.Shoot.perform += TryShoot;
        Input.instance.Shoot.cancel += StopShoot;
        Input.instance.Reload.perform += TryReload;

        ammo = _weaponData.maxAmmo;

        cooldown = new Timer(_weaponData.fireRate);
        state = WeaponState.IDLE;
    }
    
    #region Shooting
    
    private void TryShoot()
    {
        triggerPulled = true;
        if (ammo > 0 && cooldown.Ready)
        {
            StartCoroutine(nameof(TriggerPulled));
        }
    }

    private IEnumerator TriggerPulled()
    {
        while (triggerPulled && ammo > 0)
        {
            Shoot();
            yield return new WaitUntil(()=>cooldown.Ready);
        }
    }

    private void Shoot()
    {
        state = WeaponState.FIRE;
        StartCoroutine(cooldown.StartTimer());

        if(_weaponData.numOfProjectiles > 1)
            SpreadShot();
        else
            SingleShot();
        
        ammo--;
        
        if(ammo <= 0)
            TryReload();
    }

    private void SpreadShot()
    {
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
    }

    private void SingleShot()
    {
 
        var obj = PoolManager.SpawnObject(_weaponData.projectile, muzzle.position, muzzle.rotation).GetComponent<Projectile>();
        obj.Initialize(muzzle.transform.right, projectileSpeed);
    }

    private void StopShoot()
    {
        triggerPulled = false;
        state = WeaponState.IDLE;
        StopCoroutine(TriggerPulled());
    }
    
    #endregion

    #region Reloading

    private void TryReload()
    {
        if (ammo < _weaponData.maxAmmo)
        {
            StartCoroutine(nameof(Reloading));
        }
    }

    private IEnumerator Reloading()
    {
        state = WeaponState.RELOAD;
        float reloadTimer = 0;
        
        while (reloadTimer < _weaponData.reloadSpeed)
        {
            yield return new WaitForEndOfFrame();
            reloadSlider.value = reloadTimer / _weaponData.reloadSpeed;
            reloadTimer += Time.deltaTime;
        }
        
        state = WeaponState.IDLE;
        ammo = _weaponData.maxAmmo;
    }


    #endregion
}
