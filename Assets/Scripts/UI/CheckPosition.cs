using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    [SerializeField] private int changeIndex;
    private ShowText showText;

    private void Start()
    {
        showText = FindFirstObjectByType<ShowText>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        showText.SetIndexText(changeIndex);
    }
}
