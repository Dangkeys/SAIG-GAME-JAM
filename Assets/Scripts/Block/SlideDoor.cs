using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    enum direction
    {
        top = 0, down = 1, left = 2, right = 3,
    }
    [SerializeField] private direction slide;
    [SerializeField] private float distance;
    private Vector3 startTransform;
    [SerializeField] private List<HoldButton> holdButton = new List<HoldButton>();
    [SerializeField] private List<Lever> lever = new List<Lever>();
    [SerializeField] private List<Weighter> weighters = new List<Weighter>();
    [SerializeField] private List<ActivationTrigger> activationTriggers = new List<ActivationTrigger>();
    private bool isActive = false;
    private Vector3 targetPosition;
    [SerializeField] private bool needAll = false;
    [SerializeField] private int allButton;
    [SerializeField] private int activeButton;
    void Start()
    {
        Init();
    }

    void Update()
    {
        DoorSlide();
    }

    private void Init()
    {
        startTransform = transform.position;
        targetPosition = GetTargetPosition();
        allButton = holdButton.Count + lever.Count + weighters.Count + activationTriggers.Count;
        if (holdButton.Count > 0)
        {
            foreach (HoldButton button in holdButton)
            {
                if (button != null)
                {
                    button.OnActiveChanged += UpdateActive;
                }
            }
        }
        if (lever.Count > 0)
        {
            foreach (Lever lever in lever)
            {
                if (lever != null)
                {
                    lever.OnActiveChanged += UpdateActive;
                }
            }
        }
        if (weighters.Count > 0)
        {
            foreach (Weighter weighter in weighters)
            {
                if (weighter != null)
                {
                    weighter.OnActiveChanged += UpdateActive;
                }
            }
        }
        if (activationTriggers.Count > 0)
        {
            foreach (ActivationTrigger activationTrigger in activationTriggers)
            {
                if (activationTrigger != null)
                {
                    activationTrigger.OnActiveChanged += UpdateActive;
                }
            }
        }
    }

    private void UpdateActive(bool active)
    {
        if(active)
        {
            activeButton++;
        }
        else
        {
            activeButton--;
        }
        if(needAll)
        {
            if(activeButton >= allButton)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }
        else
        {
            if(activeButton >= 1)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }
    }

    private void DoorSlide()
    {
        Vector3 destination = isActive ? targetPosition : startTransform;
        if (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            float step = 10f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }

    private Vector3 GetTargetPosition()
    {
        switch (slide)
        {
            case direction.top:
                return startTransform + Vector3.up * distance;
            case direction.down:
                return startTransform + Vector3.down * distance;
            case direction.left:
                return startTransform + Vector3.left * distance;
            case direction.right:
                return startTransform + Vector3.right * distance;
            default:
                return startTransform;
        }
    }

    private bool CheckPosition(Vector3 newPosition)
    {
        switch (slide)
        {
            case direction.top:
                return newPosition.y >= startTransform.y && newPosition.y <= startTransform.y + distance &&
                       Mathf.Abs(newPosition.x - startTransform.x) < 0.1f;

            case direction.down:
                return newPosition.y <= startTransform.y && newPosition.y >= startTransform.y - distance &&
                       Mathf.Abs(newPosition.x - startTransform.x) < 0.1f;

            case direction.left:
                return newPosition.x <= startTransform.x && newPosition.x >= startTransform.x - distance &&
                       Mathf.Abs(newPosition.y - startTransform.y) < 0.1f;

            case direction.right:
                return newPosition.x >= startTransform.x && newPosition.x <= startTransform.x + distance &&
                       Mathf.Abs(newPosition.y - startTransform.y) < 0.1f;

            default:
                return false;
        }
    }
}
