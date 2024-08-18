using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
     public void QuitGame()
    {
        // Log for testing in the Unity editor
        Debug.Log("Game is exiting...");
        
        // Quit the game
        Application.Quit();
    }
}
