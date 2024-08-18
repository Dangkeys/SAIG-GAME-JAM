using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private short visionDistance;
    protected Player[] players;
    private CapsuleCollider2D capsuleCollider;
    private void Awake()
    {
        players = FindObjectsOfType<Player>();
    }

    private void Start()
    {
        initValue();
    }

    private void FixedUpdate()
    {
        int indexPlayer = ReadyToUseSkill();
        if (indexPlayer >= 0)
        {
            StopWalk();
            Attack(indexPlayer);
        }
        else
        {
            Walk();
        }
    }

    protected virtual void Attack(int indexPlayer)
    {

    }

    private void initValue()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void ChangeDirection(bool goLeft)
    {
        if (goLeft)
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

    protected void StopWalk()
    {
        rigid.velocity = Vector2.zero;
    }

    protected void Walk()
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

    protected int ReadyToUseSkill()
    {
        for (int i = 0; i < players.Length; i++)
        {
            Vector3 playerPosition = players[i].transform.position;

            bool isPlayerInVisionRange = (speed < 0 && transform.position.x - visionDistance <= playerPosition.x && transform.position.x >= playerPosition.x) ||
                                         (speed > 0 && transform.position.x + visionDistance >= playerPosition.x && transform.position.x <= playerPosition.x);

            if (isPlayerInVisionRange)
            {
                return i;
            }
        }
        return -1;
    }
}