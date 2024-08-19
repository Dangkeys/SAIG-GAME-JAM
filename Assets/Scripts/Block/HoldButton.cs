using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public bool isActive { get; private set; } = false;
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite InactiveSprite;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    private AudioManager audioManager;

    private void Start()
    {
        SpriteRenderer.sprite = InactiveSprite;
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (!isActive)
            {
                isActive = true;
                audioManager.PlaySound(3);
            }
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
