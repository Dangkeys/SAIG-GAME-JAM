using System.Collections.Generic;
using UnityEngine;

public class CheckSkillCollider : MonoBehaviour
{

    private Collider2D col;
    private Player player;

    private int colliderCount = 0;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;

        player = GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null)
            return;
        colliderCount++;
        player.CanUseSkill = false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null)
            return;
        colliderCount--;
        if (colliderCount == 0)
        {
            player.CanUseSkill = true;
        }
    }
}
