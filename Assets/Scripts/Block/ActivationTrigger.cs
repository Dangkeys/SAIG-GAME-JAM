using System;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    [SerializeField] private List<HoldButton> holdButtons = new List<HoldButton>();
    private int maxHoldButton = 0;
    private int myHoldButton = 0;
    public event Action<bool> OnActiveChanged;

    void Start()
    {
        maxHoldButton = holdButtons.Count;
        foreach(HoldButton holdButton in holdButtons)
        {
            if(holdButton != null)
            {
                holdButton.OnActiveChanged += UpdateActive;
            }
        }
    }

    private void UpdateActive(bool value)
    {
        if(value)
        {
            myHoldButton++;
        }
        else
        {
            myHoldButton--;
        }
        if(myHoldButton >= maxHoldButton)
        {
            OnActiveChanged?.Invoke(true);
        }
    }
}
