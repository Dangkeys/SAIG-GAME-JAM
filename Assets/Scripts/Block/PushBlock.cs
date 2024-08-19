using UnityEngine;

public class PushBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is CapsuleCollider2D capsuleCollider2D)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (capsuleCollider2D.size.y * collision.transform.localScale.y >= boxCollider.size.y * transform.localScale.y)
                {
                    if(player.isPlayerOne && player.CheckIfOnMacroBlock()) return;
                    rb.mass = 1f;
                }
                else
                {
                    rb.mass = 1000f;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider is CapsuleCollider2D capsuleCollider2D)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                rb.mass = 1000f;
            }
        }
    }
}
