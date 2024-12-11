using System;
using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    private static readonly int F = Animator.StringToHash("Float");
    [SerializeField] private TextMeshPro _damageText;
    [SerializeField] private Animator _animator;

    public void Initialize(float value, DamageType damageType)
    {
        _damageText.text = value.ToString();

        switch (damageType)
        {
            case DamageType.physical:
                _damageText.color = Color.white;
                break;
            case DamageType.fire:
                _damageText.color = Color.red;
                break;
            case DamageType.lightning:
                _damageText.color = new Color(238,130,238, 1);
                break;
            case DamageType.ice:
                _damageText.color = new Color(134, 214, 216, 1);
                break;
            case DamageType.dark:
                _damageText.color = new Color(128,0,128, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
        }
        
        _animator.SetTrigger(F);
    }

    public void EndFloat()
    {
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
