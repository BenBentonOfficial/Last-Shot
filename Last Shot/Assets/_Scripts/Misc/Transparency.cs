using System;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
