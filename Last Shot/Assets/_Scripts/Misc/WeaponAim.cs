using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    private void Update()
    {
        if (Input.instance.usingGamepad && Input.Aim().magnitude < 0.1f)
            return;
        
        var aimDir = Input.Aim();
        var lookDir = Vector3.up * aimDir.x + Vector3.left * aimDir.y;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }
}
