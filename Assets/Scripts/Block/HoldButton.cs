using System;
using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public bool isActive { get; private set; } = false;
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite InactiveSprite;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private LayerMask canUseSkillCheckLayerMask;
    private AudioManager audioManager;
    public event Action<bool> OnActiveChanged;

    private void Start()
    {
        SpriteRenderer.sprite = InactiveSprite;
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("skill"))return;
        if (((1 << collision.gameObject.layer) & canUseSkillCheckLayerMask.value) != 0) return;
        if (!isActive)
        {
            isActive = true;
            OnActiveChanged?.Invoke(isActive);
            audioManager.PlaySound(3);
        }
        SpriteRenderer.sprite = ActiveSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & canUseSkillCheckLayerMask.value) != 0) return;
        if (isActive)
        {
            isActive = false;
            OnActiveChanged?.Invoke(isActive);
        }
        SpriteRenderer.sprite = InactiveSprite;
    }
}
