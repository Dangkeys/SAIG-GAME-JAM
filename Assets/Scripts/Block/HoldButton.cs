using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public bool isActive { get; private set; } = false;
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite InactiveSprite;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer.sprite = InactiveSprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            isActive = true;
            SpriteRenderer.sprite = ActiveSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            isActive = false;
            SpriteRenderer.sprite = InactiveSprite;
        }
    }
}
