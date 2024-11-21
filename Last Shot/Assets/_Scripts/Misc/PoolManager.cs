using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // List of all the different pools
    public static List<PoolData> ObjectPools = new List<PoolData>();

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // find the pool that contains the selected objects
        PoolData pool = ObjectPools.Find(pool => pool.ID == objectToSpawn.name);

        // if no pool is found, create one and add it to the list of pools
        if (pool == null)
        {
            pool = new PoolData() { ID = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // find an inactive obj
        GameObject obj = pool.InactiveObjects.FirstOrDefault();

        // if no obj is found, make one. Else, activate one from the pool
        if (obj == null)
        {
            obj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            obj.transform.position = spawnPosition;
            obj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(obj);
            obj.SetActive(true);
        }

        return obj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string ID = obj.name.Substring(0, obj.name.Length - 7); // removes (clone) from check

        PoolData pool = ObjectPools.Find(pool => pool.ID == ID);

        if (pool == null)
        {
            Debug.LogWarning("Trying to deactivate an object that is not in a pool: " + ID);
            return;
        }
        
        obj.SetActive(false);
        pool.InactiveObjects.Add(obj);
    }
}

public class PoolData
{
    public string ID;
    
    // the pool
    public List<GameObject> InactiveObjects = new List<GameObject>();
}

