using System.Collections;
using TMPro;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [field: SerializeField] public float Scale { get; private set; } = 1f;
    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    [field: SerializeField] public float totalScaleTime { get; private set; } = 5f;
    [field: SerializeField] public bool isScaled { get; private set; } = false;
    [field: SerializeField] public TextMeshPro remainingTimeText;

    private Vector3 originalScale;
    private float scaleTimer;

    private void Start()
    {
        if (remainingTimeText == null)
        {
            Debug.LogError("TextMeshPro component is not assigned.");
            return;
        }

        originalScale = transform.localScale; 
        scaleTimer = totalScaleTime;
        remainingTimeText.gameObject.SetActive(false); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && !isScaled)
        {
            SetScale(player);
        }
    }

    private void SetScale(Player player)
    {
        isScaled = true;
        Scale = player.isPlayerOne ? 2f : 0.5f;
        SpriteRenderer.color = player.isPlayerOne ? Color.green : Color.yellow;
        transform.localScale = new Vector3(Scale*originalScale.x, Scale*originalScale.y, 1);
        StartCoroutine(ScaleTimer());
    }

    private IEnumerator ScaleTimer()
    {
        if (remainingTimeText == null)
        {
            Debug.LogError("TextMeshPro component is not assigned.");
            yield break;
        }

        remainingTimeText.gameObject.SetActive(true); // Show the text
        while (scaleTimer > 0)
        {
            scaleTimer -= Time.deltaTime;
            remainingTimeText.text = $"{Mathf.Ceil(scaleTimer)}"; // Show only rounded seconds
            yield return null;
        }

        ResetScale();
    }

    private void ResetScale()
    {
        transform.localScale = originalScale;
        SpriteRenderer.color = Color.white; // Reset to default color
        isScaled = false;
        scaleTimer = totalScaleTime; // Reset the timer
        remainingTimeText.gameObject.SetActive(false); // Hide the text again
    }
}
