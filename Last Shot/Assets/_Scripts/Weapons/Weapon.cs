using System;
using System.Collections;
using UnityEngine;
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

    #endregion
    
    private Vector3 _initialPosition;
    
    [SerializeField] private DamageType _damageType;

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
    }
    
    #region Shooting
    
    private void TryShoot()
    {
        _triggerPulled = true;
        
        StartCoroutine(nameof(TriggerPulled));
    }

    private IEnumerator TriggerPulled()
    {
        yield return new WaitUntil(() => _cooldown.Ready);
        while (_triggerPulled)
        {
            Shoot();
            yield return new WaitUntil(()=>_cooldown.Ready);
        }
    }

    private void Shoot()
    {
        StartCoroutine(_cooldown.StartTimer());

        if(GetTotalNumOfProjectiles() > 1)
            ArcSpreadShot();
        else
            SingleShot();

        StartCoroutine(nameof(RecoilAnimation));
    }

    private void ArcSpreadShot()
    {
        float halfSpread = spreadAngle / 2f;
        float angleStep = spreadAngle / (GetTotalNumOfProjectiles() - 1);
        
        for (int i = 0; i < GetTotalNumOfProjectiles(); i++)
        {
            /*var angle = -halfSpread + angleStep * i;
            var direction = Quaternion.Euler(0, 0, angle) * transform.right;
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            
            
            var obj = PoolManager.SpawnObject(weaponData.projectile, muzzle.position, Quaternion.Euler(0,0,angleToFace)).GetComponent<Projectile>();
            
            obj.Initialize(direction, projectileSpeed, CalcDamage(), _damageType);
            
            */
            
            SingleShot();
        }
    }

    private void SingleShot()
    {
        var dir = CalcSpread();
        var obj = PoolManager.SpawnObject(weaponData.projectile, muzzle.position, dir).GetComponent<Projectile>();
        var newDir = dir * Vector3.right;
        obj.Initialize(newDir, projectileSpeed, CalcDamage(), _damageType);
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

    private float CalcDamage()
    {
        return weaponData.damage + _player._itemCollection.GetDamageBonus();
    }

    private int GetTotalNumOfProjectiles() =>
        weaponData.numOfProjectiles + _player._itemCollection.GetProjectileNumBonus();



}
