using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : SingletonPersistent<SceneManager>
{
    // Optionally, you can have a field to track the current scene
    public string CurrentSceneName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        // Store the initial scene name
        CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void LoadScene(string sceneName)
    {
        // Optionally, you can add a loading screen or any other logic before loading the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        CurrentSceneName = sceneName;
    }

    public void ReloadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentSceneName);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(nextSceneIndex).name;
    }

    public void QuitGame()
    {
        // Handle quitting the game, either in the editor or the built application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
