using UnityEngine;

[CreateAssetMenu(menuName = "New Item / Stat Item")]
public class StatItem : Item
{
    public float increaseValue;
    public override void Equip(Player player)
    {
        
    }

    public override void Unequip(Player player)
    {
        
    }

    public override float Value()
    {
        return increaseValue;
    }
}
