using System;
using System.Collections.Generic;
using UnityEngine;

public class Weighter : MonoBehaviour
{
    public bool isActive { get; private set; } = false;
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite InactiveSprite;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private int wantWeight = 0;
    private AudioManager audioManager;
    public event Action<bool> OnActiveChanged;
    [SerializeField] private List<Player> players = new List<Player>();
    private List<Player> playersInMyWeight = new List<Player>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SpriteRenderer.sprite = InactiveSprite;
        audioManager = AudioManager.Instance;
        foreach(Player player in players)
        {
            player.OnChangeScale += UpdateWeight;
        }
    }

    private void UpdateWeight(bool value)
    {
        if(value)
        {
            int myWeight = 0;
            foreach(Player player in playersInMyWeight)
            {
                myWeight += player.MyWeight();
            }
            if(myWeight >= wantWeight)
            {
                isActive = true;
                SpriteRenderer.sprite = ActiveSprite;
                audioManager.PlaySound(3);
            }
            else
            {
                isActive = false;
                SpriteRenderer.sprite = InactiveSprite;
            }
            OnActiveChanged?.Invoke(isActive);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            playersInMyWeight.Add(player);
            UpdateWeight(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            playersInMyWeight.Remove(player);
            UpdateWeight(true);
        }
    }
}
