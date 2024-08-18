using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private bool isActive = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isActive = true;
    }
}


