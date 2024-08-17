using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private List<Vector2> position;

    private void Start()
    {
        initValue();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void initValue()
    {
        rigid = GetComponent<Rigidbody2D>();
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

    private void Walk()
    {
        if (transform.position.x < position[0].x)
        {
            ChangeDirection(true);
        }
        else if (transform.position.x > position[1].x)
        {
            ChangeDirection(false);
        }
        Vector2 movement = new Vector2(speed * Time.fixedDeltaTime * Time.timeScale, 0);
        rigid.velocity = movement;
    }
}