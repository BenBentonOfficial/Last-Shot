using System;
using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    private static readonly int F = Animator.StringToHash("Float");
    [SerializeField] private TextMeshPro _damageText;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Color[] _colors;

    public void Initialize(float value, DamageType damageType)
    {
        _damageText.text = value.ToString();
        
        _damageText.color = _colors[(int)damageType];
        
        _animator.SetTrigger(F);
    }

    public void EndFloat()
    {
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
