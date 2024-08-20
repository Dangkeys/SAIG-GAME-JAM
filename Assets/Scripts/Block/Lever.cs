using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isActive { get; private set; } = false;
    private bool working = false;
    private bool goRight = false;
    private BoxCollider2D boxCollider2D;
    private AudioManager audioManager;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (working)
        {
            ChangeAngle();
        }
    }

    private void ChangeAngle()
    {
        if (goRight)
        {
            transform.Rotate(Vector3.back);
            if (transform.eulerAngles.z >= 310 && transform.eulerAngles.z <= 315)
            {
                working = false;
                isActive = true;
            }
        }
        else
        {
            transform.Rotate(Vector3.forward);
            if (transform.eulerAngles.z >= 45 && transform.eulerAngles.z <= 50)
            {
                working = false;
                isActive = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Collider2D topLeft = GetColliderAtTopLeft(boxCollider2D);
            Collider2D topRight = GetColliderAtTopRight(boxCollider2D);
            if (topLeft && !isActive)
            {
                goRight = true;
                working = true;
                audioManager.PlaySound(2);
            }
            else if (topRight && isActive)
            {
                goRight = false;
                working = true;
                audioManager.PlaySound(2);
            }
        }
    }

    private Collider2D GetColliderAtTopLeft(BoxCollider2D boxCollider)
    {
        Vector2 TopLeft = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.max.y);
        Collider2D colliderAtTopLeft = Physics2D.OverlapPoint(TopLeft);

        return colliderAtTopLeft;
    }

    private Collider2D GetColliderAtTopRight(BoxCollider2D boxCollider)
    {
        Vector2 TopRight = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y);
        Collider2D colliderAtTopRight = Physics2D.OverlapPoint(TopRight);

        return colliderAtTopRight;
    }

    private void OnDrawGizmos()
    {
        // Initialize boxCollider2D if it's null (for edit mode)
        if (boxCollider2D == null)
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        if (boxCollider2D == null) return;

        // Draw top-left gizmo
        Vector2 topLeft = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.y);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(topLeft, 0.1f);

        // Draw top-right gizmo
        Vector2 topRight = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.max.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(topRight, 0.1f);
    }

}
