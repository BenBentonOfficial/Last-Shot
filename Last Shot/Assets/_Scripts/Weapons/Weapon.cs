using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum DamageType
{
    Physical,
    Fire,
    Lightning,
    Ice,
    Dark
}

public class Weapon : MonoBehaviour
{
    public Player _player;
    
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform muzzle;

    private Timer _cooldown;

    private bool _triggerPulled;

    #region WeaponStats
    
    public float spreadAngle;
    public float projectileSpeed;
    public float recoilDistance;
    public float recoilSpeed;

    public float maxAmmo;
    public float currentAmmo;
    public float reloadTime;
    public float reloadLength;

    public int peirce;

    #endregion

    [SerializeField] private GameObject ReloadUI;
    [SerializeField] private Slider reloadSlider;
    
    private Vector3 _initialPosition;
    
    [SerializeField] private DamageType _damageType;
    [SerializeField] private GameObject gunFlash;

    private Dictionary<Type, int> bulletUpgrades = new();

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        Input.instance.Shoot.perform += TryShoot;
        Input.instance.Shoot.cancel += StopShoot;
        _cooldown = new Timer(weaponData.fireRate);

        _initialPosition = transform.localPosition;

        currentAmmo = maxAmmo;
    }

    public void Equip(WeaponData newData)
    {
        weaponData = newData;
        
    }
    
    #region Shooting
    
    private void TryShoot()
    {
        _triggerPulled = true;
        
        StartCoroutine(nameof(TriggerPulled));
    }
    
    private IEnumerator Reload()
    {
        Debug.Log("reloading");
        ReloadUI.SetActive(true);
        
        reloadTime = 0;

        while (reloadTime < reloadLength)
        {
            yield return new WaitForEndOfFrame();
            reloadSlider.value = reloadTime / reloadLength;
            reloadTime += Time.deltaTime;
        }
        
        currentAmmo = maxAmmo;
        ReloadUI.SetActive(false);
        
        Debug.Log("Ready to fire");
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.O))
        {
            UnlockBehavior<BulletSplit>();
        }
    }

    private IEnumerator TriggerPulled()
    {
        yield return new WaitUntil(() => _cooldown.Ready && currentAmmo > 0);
        while (_triggerPulled)
        {
            
            Shoot();
            yield return new WaitUntil(()=>_cooldown.Ready && currentAmmo > 0);
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
            return;
        
        StartCoroutine(_cooldown.StartTimer());

        currentAmmo--;
        
        if (currentAmmo <= 0)
            StartCoroutine(Reload());

        if(GetTotalNumOfProjectiles() > 1)
            ArcSpreadShot();
        else
            SingleShot();

        StartCoroutine(nameof(RecoilAnimation));
    }

    private void ArcSpreadShot()
    {
        for (int i = 0; i < GetTotalNumOfProjectiles(); i++)
        {
            SingleShot();
        }
    }

    private void SingleShot()
    {
        var dir = CalcSpread();
        var obj = PoolManager.SpawnObject(weaponData.projectile, muzzle.position, dir).GetComponent<Projectile>();
        foreach (var upgrade in bulletUpgrades)
        {
            var upgradeType = upgrade.Key;
            var level = upgrade.Value;

            if (obj.gameObject.GetComponent(upgradeType) == null)
            {
                var newBehavior = (BulletBehaviour)obj.gameObject.AddComponent(upgradeType);
                newBehavior.SetLevel(level);
            }
               
        }
        var newDir = dir * Vector3.right;
        obj.Initialize(newDir, projectileSpeed, CalcDamage(), peirce, _damageType);
        
        // FLASH
        // var flash = PoolManager.SpawnObject(gunFlash, muzzle.position, dir).GetComponent<GunFlash>();
        // flash.Initialize(Random.Range(0, 2));
    }

    private Quaternion CalcSpread()
    {
        var angle = Random.Range(-spreadAngle, spreadAngle);
        return muzzle.rotation * Quaternion.Euler(0,0,angle);
    }

    private void StopShoot()
    {
        _triggerPulled = false;
        StopCoroutine(nameof(TriggerPulled));
    }
    
    private IEnumerator RecoilAnimation()
    {
        // Move the gun backward
        Vector3 recoilPosition = _initialPosition - Vector3.right * recoilDistance;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(_initialPosition, recoilPosition, progress);
            yield return null;
        }

        // Move the gun back to its original position
        progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(recoilPosition, _initialPosition, progress);
            yield return null;
        }
    }
    
    #endregion

    public void UnlockBehavior<T>() where T : BulletBehaviour
    {
        if(!bulletUpgrades.ContainsKey(typeof(T)))
            bulletUpgrades.Add(typeof(T), 1);
    }

    public void UpgradeBehavior<T>(int amount = 1) where T : BulletBehaviour
    {
        var type = typeof(T);

        if (!bulletUpgrades.ContainsKey(type))
        {
            Debug.LogWarning(type.Name + " not unlocked yet!");
            return;
        }
        
        bulletUpgrades[type] += amount;
        Debug.Log(type.Name + " has been upgraded to level: " + bulletUpgrades[type]);
        
    }

    private float CalcDamage()
    {
        return weaponData.damage + _player._itemCollection.GetDamageBonus();
    }

    private int GetTotalNumOfProjectiles() =>
        weaponData.numOfProjectiles + _player._itemCollection.GetProjectileNumBonus();



}
