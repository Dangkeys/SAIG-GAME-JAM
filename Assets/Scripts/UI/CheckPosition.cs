using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    [SerializeField] private int changeIndex;
    [SerializeField] private ShowText showText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        showText.SetIndexText(changeIndex);
    }
}
