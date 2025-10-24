using UnityEngine;

public abstract class BulletBehaviour : MonoBehaviour
{
    protected int Level = 0;
    public abstract void OnHit(GameObject target, Vector2 hitPoint);

    public abstract void SetLevel(int lvl);
}
