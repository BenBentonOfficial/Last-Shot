using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionReloadUI : MonoBehaviour
{
    private Slider reloadSlider;
    

    private void OnEnable()
    {
        reloadSlider = GetComponent<Slider>();
    }

    private void CheckReload()
    {
        
    }
}
