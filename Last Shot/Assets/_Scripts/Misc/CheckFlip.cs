using UnityEngine;

public class CheckFlip : MonoBehaviour
{
    [SerializeField] private bool flipY;

    private Vector3 norm = new Vector3(1, 1, 1);
    private Vector3 flippedX = new Vector3(-1, 1, 1);
    private Vector3 flippedY = new Vector3(1, -1, 1);
    private void Update()
    {
        if (flipY)
        {
            transform.localScale = Input.Aim().x >= 0 ? norm : flippedY;
        }
        else
        {
            transform.localScale = Input.Aim().x >= 0 ? norm : flippedX;
        }
    }
}
