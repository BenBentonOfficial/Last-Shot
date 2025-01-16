using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemCollection
{
    public List<Item> items;

    public float GetDamageBonus()
    {
        var damageBonus = 0f;
        foreach (var item in items)
        {
            if (item.affectedStat == AffectedStat.damage)
            {
                damageBonus += item.Value();
            }
        }

        return damageBonus;
    }

    public int GetProjectileNumBonus()
    {
        int projectileNumBonus = 0;

        foreach (var item in items)
        {
            if (item.affectedStat == AffectedStat.projectileCount)
            {
                projectileNumBonus += (int)item.Value();
            }
        }

        return projectileNumBonus;
    }
    
    public void EquipItem(Item item, Player player)
    {
        item.Initialize(player);
        items.Add(item);
    }
}
