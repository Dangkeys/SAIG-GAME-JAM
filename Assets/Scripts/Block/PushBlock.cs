// ...existing code...
using System;
using System.Linq;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Player player1;
    [SerializeField] private LayerMask playerLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player1 = FindObjectsByType<Player>(FindObjectsSortMode.None)
            .FirstOrDefault(p => p.IsPlayerOne);
        if (player1 != null)
            player1.OnChangeScale += ShouldChangeMass;
    }

    private void ShouldChangeMass(bool obj = true)
    {
        if (boxCollider == null || rb == null) return;

        Vector2 center = transform.TransformPoint(boxCollider.offset);
        Vector2 size = Vector2.Scale(boxCollider.size, new Vector2(transform.lossyScale.x, transform.lossyScale.y));
        float angle = transform.eulerAngles.z;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(center, size, angle, playerLayerMask.value);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Player player))
            {
                if (player == player1 && player.IsScaled)
                {
                    rb.mass = 1f;
                    return;
                }
            }
        }
        rb.mass = 1000f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShouldChangeMass();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ShouldChangeMass();
    }

    private void OnDestroy()
    {
        if (player1 != null)
            player1.OnChangeScale -= ShouldChangeMass;
    }
}