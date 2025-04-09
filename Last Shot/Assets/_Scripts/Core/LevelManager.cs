using System;
using UnityEngine;

public  class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;

    private int currentLevel;

    private static int currentExp;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        currentLevel = 1;
    }

    public static void GainExperience(int amount)
    {
        currentExp += amount;
    }
}
