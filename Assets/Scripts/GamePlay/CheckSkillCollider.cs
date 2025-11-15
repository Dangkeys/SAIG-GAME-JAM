using System.Collections.Generic;
using UnityEngine;

public class CheckSkillCollider : MonoBehaviour
{

    private Collider2D col;
    private Player player;

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
        player.CanUseSkill = false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null)
            return;
        player.CanUseSkill = true;
    }
}
