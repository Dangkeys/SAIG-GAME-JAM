using System;
using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    [SerializeField]private uint speed = 1;
    private Vector2 direction;
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb;
    private AudioManager audioManager;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = AudioManager.Instance;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity += direction * speed * Time.fixedDeltaTime;
    }

    public void SetTarget(Vector3 position)
    {
        Vector2 directionToTarget = (position - transform.position).normalized;
        direction = directionToTarget;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.Die();
            }
            audioManager.PlaySound(12);
            Destroy(gameObject);
        }
    }
}
