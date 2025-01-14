using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public abstract class SkillData : ScriptableObject
{
    public string skillName;

    public abstract void Activate();
    public abstract void Deactivate();
}
