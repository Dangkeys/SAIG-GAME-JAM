using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private bool isActive = false;
    private bool working = false;
    private bool goRight = false;
    private BoxCollider2D boxCollider2D;


    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Debug.Log("Z Rotation: " + transform.eulerAngles.z);
        if (working)
        {
            ChangeAngle();
        }
    }

    private void ChangeAngle()
    {
        if(goRight)
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
            if(topLeft && !isActive)
            {
                goRight = true;
                working = true;
            }
            else if(topRight && isActive)
            {
                goRight = false;
                working = true;
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
}


