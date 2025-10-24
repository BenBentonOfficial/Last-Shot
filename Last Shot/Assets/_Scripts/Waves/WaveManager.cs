using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private CircleCollider2D spawnCircle;

    public GameObject player;
    public GameObject enemy;
    
    
    public static List<Transform> ActiveEnemies = new List<Transform>();

    private void Start()
    {
        spawnCircle = GetComponentInChildren<CircleCollider2D>();

        StartCoroutine(SpawnWave());
    }

    public static void Register(Transform enemy)
    {
        if(!ActiveEnemies.Contains(enemy))
            ActiveEnemies.Add(enemy);
    }

    public static void Unregister(Transform enemy)
    {
        ActiveEnemies.Remove(enemy);
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            var spawned = PoolManager.SpawnObject(enemy, GetSpawnPosition(), Quaternion.identity).transform;
            Register(spawned);
            yield return new WaitForSeconds(0.8f);
        }
        
    }
    
    private Vector3 GetSpawnPosition()
    {
        // get quaternion of random rotation in all 3 axes
        Quaternion quat = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));

        // get forward vector
        Vector3 forward = quat * Vector2.right;

        // get random distance from center, and move that way to find position
        Vector3 position = forward * spawnCircle.radius;

        return position;
    }
}

public class WaveData : ScriptableObject
{
    
}
