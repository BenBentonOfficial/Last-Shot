using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon" )]
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public GameObject projectile; // how to do pooling
    
    public int maxAmmo;
    public int numOfProjectiles;
    public float fireRate;
    public float reloadSpeed;


}
