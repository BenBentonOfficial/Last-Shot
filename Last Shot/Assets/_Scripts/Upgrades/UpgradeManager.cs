using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    
    [SerializeField] private GameObject _upgradeUI;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public static void TriggerUpgrade()
    {
        instance._upgradeUI.SetActive(true);
    }
}
