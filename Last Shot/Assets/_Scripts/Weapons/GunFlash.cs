using System;
using UnityEngine;

public class GunFlash : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize(int layerIndex)
    {
        animator.SetLayerWeight(layerIndex, 1);
    }
    
    public void EndAnim()
    {
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
