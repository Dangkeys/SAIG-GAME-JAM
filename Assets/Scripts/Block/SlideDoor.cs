using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    enum direction
    {
        top=0, down=1, left=2, right=3,
    }
    [SerializeField] private direction slide;
    [SerializeField] private float distance;
    private Vector3 startTransform;
    [SerializeField] private List<HoldButton> holdButton;
    [SerializeField] private List<Lever> lever;
    private bool isActive = false;
    private Vector3 targetPosition;
    void Start()
    {
        startTransform = transform.position;
        targetPosition = GetTargetPosition();
    }

    void Update()
    {
        isActive = CheckIsActive();
        DoorSlide();
    }

    private bool CheckIsActive()
    {
        foreach (HoldButton button in holdButton)
        {
            if (button.isActive)
                return true;
        }

        foreach (Lever lever in lever)
        {
            if (lever.isActive)
                return true;
        }

        return false;
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
