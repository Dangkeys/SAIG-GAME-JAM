using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    [SerializeField]private uint speed = 1;
    private Vector3 target;
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += target * speed * Time.fixedDeltaTime;
    }

    public void SetTarget(Vector3 position)
    {
        target = position;
    }
}
