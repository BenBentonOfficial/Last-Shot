using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class BulletSplit : BulletBehaviour
{
    public override void OnHit(GameObject target, Vector2 hitPoint)
    {
        var nearestEnemies = FindNearestEnemyDirections(hitPoint, target.transform, Level);

        foreach (var targetDirection in nearestEnemies)
        {
            var obj = PoolManager.SpawnObject(this.gameObject, hitPoint, Rotation(targetDirection) ).GetComponent<Projectile>();
            var newDir = targetDirection * Vector3.right;
            obj.Initialize(targetDirection, 30, 1, 0, target.transform);
            Destroy(obj.GetComponent<BulletSplit>());
        }
        
    }

    public override void SetLevel(int lvl)
    {
        Level = lvl;
    }

    public Quaternion Rotation(Vector2 direction)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    
    List<Vector2> FindNearestEnemyDirections(Vector2 origin, Transform excludeEnemy, int count)
    {
        var nearest = WaveManager.ActiveEnemies
            .Where(e => e != null && e != excludeEnemy)
            .OrderBy(e => ((Vector2)e.position - origin).sqrMagnitude)
            .Take(count);

        List<Vector2> directions = new List<Vector2>();
        foreach (var enemy in nearest)
        {
            Vector2 dir = ((Vector2)enemy.position - origin).normalized;
            directions.Add(dir);
        }

        return directions;
    }

}
