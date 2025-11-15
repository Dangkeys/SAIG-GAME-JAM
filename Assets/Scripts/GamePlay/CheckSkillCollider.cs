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
        player.CanUseSkill = false;
    }
    void OnTriggerExit(Collider other)
    {
        player.CanUseSkill = true;
    }
}
