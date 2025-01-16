using System;
using UnityEngine;

public enum AffectedStat
{
    fireRate,
    projectileCount,
    damage,
    ability
}
public abstract class Item : ScriptableObject
{
    public AffectedStat affectedStat;
    public abstract void Initialize(Player player);

    public abstract float Value(); 

}
