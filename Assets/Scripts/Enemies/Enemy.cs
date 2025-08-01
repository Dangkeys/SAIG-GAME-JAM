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
    private GameObject isTracking = null;
    private SpriteRenderer spriteRenderer;
    protected AudioManager audioManager;
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
        Walk();
        if (indexPlayer >= 0)
        {
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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioManager = AudioManager.Instance;
    }

    private void ChangeDirection(bool goLeft)
    {
        if (goLeft)
        {
            spriteRenderer.flipX = false;
            speed = (speed < 0) ? -speed : speed;
        }
        else
        {
            spriteRenderer.flipX = true;
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

    protected void Walk()
    {
        isTracking = Tracking();
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
        else if(isTracking != null)
        {
            if(isTracking.transform.position.x >= transform.position.x)
            {
                ChangeDirection(true);
            }
            else if(isTracking.transform.position.x < transform.position.x)
            {
                ChangeDirection(false);
            }
            if(isTracking.transform.position.x < transform.position.x - visionDistance || isTracking.transform.position.x > transform.position.x + visionDistance)
            {
                isTracking = null;
            }
        }
        Vector2 movement = new Vector2(speed * Time.fixedDeltaTime * Time.timeScale, 0);
        rigid.linearVelocity = movement;
    }

    private GameObject Tracking()
    {
        float distance = visionDistance;
        GameObject isTracking = null;
        foreach(var player in players)
        {
            bool isUnderEnemy = transform.position.y <= player.transform.position.y;
            float newDistance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > newDistance && isUnderEnemy)
            {
                distance = newDistance;
                isTracking = player.gameObject;
            }
        }
        return isTracking;
    }

    protected int ReadyToUseSkill()
    {
        for (int i = 0; i < players.Length; i++)
        {
            Vector3 playerPosition = players[i].transform.position;
            bool isUnderEnemy = transform.position.y <= playerPosition.y;
            bool isPlayerInVisionRange = (speed < 0 && transform.position.x - visionDistance <= playerPosition.x && transform.position.x >= playerPosition.x && isUnderEnemy) ||
                                         (speed > 0 && transform.position.x + visionDistance >= playerPosition.x && transform.position.x <= playerPosition.x && isUnderEnemy);

            if (isPlayerInVisionRange)
            {
                return i;
            }
        }
        return -1;
    }
}
