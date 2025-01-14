using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon" )]
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public GameObject projectile;
    
    [Header("Starting Weapon Stats")]
    [Range(1, 10)] public int numOfProjectiles;
    [Range(0.05f, 2)] public float fireRate;
    [Range(1, 90)] public float spreadAngle;
    [Range(10, 100)] public float projectileSpeed;
    [Range(0, 0.5f)] public float recoilDistance;
    [Range(15, 50)] public float recoilSpeed;


}
