using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddMouseTrigger : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private AudioManager audioManager;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        buttons = FindObjectsOfType(typeof(Button)) as Button[];
        SetButton();
        audioManager = AudioManager.Instance;
    }

    private void SetButton()
    {
        foreach (var button in buttons)
        {
            EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry clickEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            clickEntry.callback.AddListener((data) => { OnButtonClick(); });

            EventTrigger.Entry enterEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            enterEntry.callback.AddListener((data) => { OnPointerEnter(); });

            eventTrigger.triggers.Add(clickEntry);
            eventTrigger.triggers.Add(enterEntry);
        }
    }
    void OnButtonClick()
    {
        audioManager.PlaySound(7);
    }

    void OnPointerEnter()
    { 
        audioManager.PlaySound(8);
    }
}
