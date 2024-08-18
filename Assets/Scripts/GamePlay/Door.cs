using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isPlayerOneDoor = true;

    private static bool isPlayerOneOnDoor = false;
    private static bool isPlayerTwoOnDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (isPlayerOneDoor && player.isPlayerOne)
            {
                isPlayerOneOnDoor = true;
            }
            else if (!isPlayerOneDoor && !player.isPlayerOne)
            {
                isPlayerTwoOnDoor = true;
            }
        }

        CheckPlayersOnDoors();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (isPlayerOneDoor && player.isPlayerOne)
            {
                isPlayerOneOnDoor = false;
            }
            else if (!isPlayerOneDoor && !player.isPlayerOne)
            {
                isPlayerTwoOnDoor = false;
            }
        }
    }

    private void CheckPlayersOnDoors()
    {
        if (isPlayerOneOnDoor && isPlayerTwoOnDoor)
        {
            ChangeScene();
        }
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f); 
        SceneManager.Instance.LoadNextScene();
    }
}
