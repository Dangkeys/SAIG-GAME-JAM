using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour
{
    [SerializeField] private bool isActive = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        isActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
    }
}
