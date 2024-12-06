using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform muzzle;

    private Timer cooldown;

    private bool triggerPulled;

    public float spreadAngle;
    public float projectileSpeed;
    public float recoilDistance;
    public float recoilSpeed;
    
    private Vector3 initialPosition;

    private void Start()
    {
        Input.instance.Shoot.perform += TryShoot;
        Input.instance.Shoot.cancel += StopShoot;
        
        cooldown = new Timer(_weaponData.fireRate);

        initialPosition = transform.localPosition;
    }
    
    #region Shooting
    
    private void TryShoot()
    {
        triggerPulled = true;
        
        StartCoroutine(nameof(TriggerPulled));
    }

    private IEnumerator TriggerPulled()
    {
        yield return new WaitUntil(() => cooldown.Ready);
        while (triggerPulled)
        {
            Shoot();
            yield return new WaitUntil(()=>cooldown.Ready);
        }
    }

    private void Shoot()
    {
        StartCoroutine(cooldown.StartTimer());

        if(_weaponData.numOfProjectiles > 1)
            ArcSpreadShot();
        else
            SingleShot();

        StartCoroutine(nameof(RecoilAnimation));
    }

    private void ArcSpreadShot()
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
        var dir = CalcSpread();
        var obj = PoolManager.SpawnObject(_weaponData.projectile, muzzle.position, dir).GetComponent<Projectile>();
        var newDir = dir * Vector3.right;
        obj.Initialize(newDir, projectileSpeed);
    }

    private Quaternion CalcSpread()
    {
        var angle = Random.Range(-spreadAngle, spreadAngle);
        return muzzle.rotation * Quaternion.Euler(0,0,angle);
    }

    private void StopShoot()
    {
        triggerPulled = false;
        StopCoroutine(nameof(TriggerPulled));
    }
    
    private IEnumerator RecoilAnimation()
    {
        // Move the gun backward
        Vector3 recoilPosition = initialPosition - Vector3.right * recoilDistance;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(initialPosition, recoilPosition, progress);
            yield return null;
        }

        // Move the gun back to its original position
        progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(recoilPosition, initialPosition, progress);
            yield return null;
        }
    }
    
    #endregion
    
    
    
}
