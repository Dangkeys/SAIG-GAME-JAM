using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private short visionDistance;
    [SerializeField] private GameObject player;
    private CapsuleCollider2D capsuleCollider;

    private void Start()
    {
        initValue();
    }

    private void FixedUpdate()
    {
        Walk();
        Debug.Log(ReadyToUseSkill());
    }

    private void initValue()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void ChangeDirection(bool goLeft)
    {
        if(goLeft)
        {
            speed = (speed < 0) ? -speed : speed;
        }
        else
        {
            speed = (speed > 0) ? -speed : speed;
        }
    }

    private Collider2D GetColliderAtBottomLeft(CapsuleCollider2D capsuleCollider)
    {
        Vector2 bottomLeft = new Vector2(capsuleCollider.bounds.min.x, capsuleCollider.bounds.min.y - capsuleCollider.size.y / 2f);
        Collider2D colliderAtBottomLeft = Physics2D.OverlapPoint(bottomLeft);

        return colliderAtBottomLeft;
    }

    private Collider2D GetColliderAtBottomRight(CapsuleCollider2D capsuleCollider)
    {
        Vector2 bottomRight = new Vector2(capsuleCollider.bounds.max.x, capsuleCollider.bounds.min.y - capsuleCollider.size.y / 2f);
        Collider2D colliderAtBottomRight = Physics2D.OverlapPoint(bottomRight);

        return colliderAtBottomRight;
    }

    private void Walk()
    {
        Collider2D left = GetColliderAtBottomLeft(capsuleCollider);
        Collider2D right = GetColliderAtBottomRight(capsuleCollider);
        if (left == null)
        {
            ChangeDirection(true);
        }
        else if (right == null)
        {
            ChangeDirection(false);
        }
        Vector2 movement = new Vector2(speed * Time.fixedDeltaTime * Time.timeScale, 0);
        rigid.velocity = movement;
    }

    private bool ReadyToUseSkill()
    {
        if (speed < 0 && transform.position.x - visionDistance <= player.transform.position.x && transform.position.x >= player.transform.position.x)
        {
            return true;
        }
        else if (speed > 0 && transform.position.x + visionDistance >= player.transform.position.x && transform.position.x <= player.transform.position.x)
        {
            return true;
        }
        return false;
    }
}