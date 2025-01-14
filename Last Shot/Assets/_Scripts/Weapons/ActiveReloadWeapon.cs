using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActiveReloadWeapon : MonoBehaviour
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

    #region Reload

    [SerializeField] private GameObject ReloadUI;
    [SerializeField] private RectTransform sliderRect;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private RectTransform perfectReloadImage;
    [SerializeField] private RectTransform activeReloadImage;
    

    [SerializeField] private float perfectReload = 1.8f;
    [SerializeField] private float activeReload = 2.25f;
    [SerializeField] private float normalReload = 3f;
    [SerializeField] private float failedReload = 4.1f;

    private float reloadTime;
    private bool reloading;
    private bool reloadFailed;
    
    private Coroutine reloadCoroutine;
    
    private void InitializeReloadSlider()
    {
        reloadSlider.value = 0;

        perfectReloadImage.anchoredPosition = new Vector2((perfectReload / normalReload) * sliderRect.rect.width, 0);
        activeReloadImage.anchoredPosition = new Vector2((activeReload / normalReload) * sliderRect.rect.width, 0);

    }

    #endregion

    
    private void Start()
    {
        Input.instance.Shoot.perform += TryShoot;
        Input.instance.Shoot.cancel += StopShoot;
        Input.instance.Reload.perform += TryReload;
        

        cooldown = new Timer(_weaponData.fireRate);
        ReloadUI.SetActive(false);
    }

    private void TryReload()
    {
        if (reloading && !reloadFailed)
        {
            if (reloadTime >= perfectReload && reloadTime < activeReload)
            {
                Debug.Log("PERFECT!");
                reloadTime = normalReload;
            }
            else if (reloadTime >= activeReload && reloadTime < normalReload)
            {
                Debug.Log("ACTIVE!");
                reloadTime = normalReload;
            }
            else if (reloadTime < perfectReload)
            {
                Debug.Log("BAD");
                StopCoroutine(reloadCoroutine);
                StartCoroutine(FailedReload());
            }
        }
        
    }


    private IEnumerator FailedReload()
    {
        reloadFailed = true;
        reloading = true;

        reloadTime = 0;

        while (reloadTime < failedReload)
        {
            yield return new WaitForEndOfFrame();
            reloadSlider.value = reloadTime / failedReload;
            reloadTime += Time.deltaTime;
        }

        reloadFailed = false;
        reloading = false;
        ReloadUI.SetActive(false);
        
    }

    private IEnumerator Reload()
    {
        Debug.Log("reloading");
        InitializeReloadSlider();
        ReloadUI.SetActive(true);

        reloading = true;
        reloadTime = 0;

        while (reloadTime < normalReload && reloading)
        {
            yield return new WaitForEndOfFrame();
            reloadSlider.value = reloadTime / normalReload;
            reloadTime += Time.deltaTime;
        }
        
        ReloadUI.SetActive(false);
        reloading = false;
        
        Debug.Log("Ready to fire");
    }

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
        StartCoroutine(cooldown.StartTimer());

        if(_weaponData.numOfProjectiles > 1)
            SpreadShot();
        else
            SingleShot();
        
        ammo--;
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
            obj.Initialize(direction, projectileSpeed, 1, DamageType.Dark);
        }
    }

    private void SingleShot()   
    {
 
        var obj = PoolManager.SpawnObject(_weaponData.projectile, muzzle.position, muzzle.rotation).GetComponent<Projectile>();
        obj.Initialize(muzzle.transform.right, projectileSpeed, 1, DamageType.Dark);
    }

    private void StopShoot()
    {
        triggerPulled = false;
        StopCoroutine(TriggerPulled());
    }
    
}
