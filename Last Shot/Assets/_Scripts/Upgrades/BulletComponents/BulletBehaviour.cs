using UnityEngine;

public abstract class BulletBehaviour : MonoBehaviour
{
    public abstract void OnHit(GameObject target, Vector2 hitPoint);
}
