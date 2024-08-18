using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D capsuleCollider2D)
        {
            if (capsuleCollider2D.size.x * collision.transform.localScale.x >= boxCollider.size.x * transform.localScale.x)
            {
                boxCollider.isTrigger = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is CapsuleCollider2D capsuleCollider2D)
        {
            if (capsuleCollider2D.size.x * collision.transform.localScale.x < boxCollider.size.x * transform.localScale.x)
            {
                boxCollider.isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D capsuleCollider2D)
        {
            if (capsuleCollider2D.size.x * collision.transform.localScale.x < boxCollider.size.x * transform.localScale.x)
            {
                boxCollider.isTrigger = false;
            }
        }
    }
}
