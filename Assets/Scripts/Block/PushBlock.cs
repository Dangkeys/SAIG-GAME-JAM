using UnityEngine;

public class PushBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 scale;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.localScale.x > scale.x && collision.transform.localScale.y > scale.y)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.localScale.x > scale.x && collision.transform.localScale.y > scale.y)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.localScale.x > scale.x && collision.transform.localScale.y > scale.y)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
