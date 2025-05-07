using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;
    
    private static int _experience;
    private static int _currentLevel;
    private static int _experienceToNextLevel;
    private static float _experienceToNextLevelMultiplier = 1.25f;
    
    public static int CurrentLevel => _currentLevel;
    public static int Experience => _experience;
    public static int ExperienceToNextLevel => _experienceToNextLevel;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        _currentLevel = 1;
        _experienceToNextLevel = 5;
    }

    public static void GainExperience(float amount)
    {
        _experience += Mathf.FloorToInt(amount);

        if (_experience >= _experienceToNextLevel)
        {
            _currentLevel++;
            _experienceToNextLevel = Mathf.FloorToInt(_experienceToNextLevel * _experienceToNextLevelMultiplier);
        }
    }
}
