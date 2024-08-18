using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public bool isActive { get; private set; } = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            isActive = false;
        }
    }
}
