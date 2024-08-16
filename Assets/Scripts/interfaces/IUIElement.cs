using UnityEngine;

public interface IUIElement
{
    void Show()
    {
        // Default implementation
        if (this is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(true);
        }
    }

    void Hide()
    {
        // Default implementation
        if (this is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
        }
    }
}
