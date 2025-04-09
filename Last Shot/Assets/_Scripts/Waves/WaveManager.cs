using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private CircleCollider2D spawnCircle;

    public GameObject enemy;

    private void Start()
    {
        spawnCircle = GetComponentInChildren<CircleCollider2D>();

        StartCoroutine(SpawnWave());
    }

    public void StartWave()
    {
        
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            PoolManager.SpawnObject(enemy, GetSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
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
