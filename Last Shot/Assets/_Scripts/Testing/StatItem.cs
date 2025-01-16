using UnityEngine;

[CreateAssetMenu(menuName = "New Item / Stat Item")]
public class StatItem : Item
{
    public float increaseValue;
    public override void Initialize(Player player)
    {
        
    }

    public override float Value()
    {
        return increaseValue;
    }
}
