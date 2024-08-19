using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isPlayerOneDoor = true;
    [SerializeField] private Transform snapPosition;
    [SerializeField] private Sprite noPlayerOnDoorSprite;
    [SerializeField] private Sprite playerOnDoorSprite;
    [SerializeField]  SpriteRenderer spriteRenderer;   
    private static bool isPlayerOneOnDoor = false;
    private static bool isPlayerTwoOnDoor = false;

    private static Player playerOne;
    private static Player playerTwo;

    private static bool playersSnapped = false; // Flag to check if players have already been snapped
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (isPlayerOneDoor && player.isPlayerOne)
            {
                isPlayerOneOnDoor = true;
                playerOne = player;
                spriteRenderer.sprite = playerOnDoorSprite;
            }
            else if (!isPlayerOneDoor && !player.isPlayerOne)
            {
                isPlayerTwoOnDoor = true;
                playerTwo = player;
                spriteRenderer.sprite = playerOnDoorSprite;
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
                playerOne = null;
                spriteRenderer.sprite = noPlayerOnDoorSprite;
            }
            else if (!isPlayerOneDoor && !player.isPlayerOne)
            {
                isPlayerTwoOnDoor = false;
                playerTwo = null;
                spriteRenderer.sprite = noPlayerOnDoorSprite;
            }

            // Reset the snap flag if any player exits the door
            playersSnapped = false;
        }
    }

    private void CheckPlayersOnDoors()
    {
        if (isPlayerOneOnDoor && isPlayerTwoOnDoor && !playersSnapped)
        {
            SnapPlayersToDoors();
            StopPlayersMovement();
            audioManager.PlaySound(4);
            StartCoroutine(WaitAndChangeScene());
        }
    }

    private void SnapPlayersToDoors()
    {
        playersSnapped = true;

        if (playerOne != null)
        {
            playerOne.transform.position = GetClosestDoor(playerOne).snapPosition.position;
        }

        if (playerTwo != null)
        {
            playerTwo.transform.position = GetClosestDoor(playerTwo).snapPosition.position;
        }
    }

    private Door GetClosestDoor(Player player)
    {
        Door[] doors = FindObjectsOfType<Door>();
        Door closestDoor = null;
        float closestDistance = float.MaxValue;

        foreach (Door door in doors)
        {
            float distance = Vector2.Distance(player.transform.position, door.snapPosition.position);
            if (distance < closestDistance && door.isPlayerOneDoor == player.isPlayerOne)
            {
                closestDistance = distance;
                closestDoor = door;
            }
        }

        return closestDoor;
    }

    private void StopPlayersMovement()
    {
        if (playerOne != null)
        {
            playerOne.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop Player 1's movement
            playerOne.enabled = false; // Disable Player 1's script to stop input
        }

        if (playerTwo != null)
        {
            playerTwo.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop Player 2's movement
            playerTwo.enabled = false; // Disable Player 2's script to stop input
        }
    }

    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(.5f);
        PlayerPrefs.SetInt("Win", SceneManager.Instance.GetCurrentScene());
        SceneManager.Instance.LoadNextScene();
    }
}
