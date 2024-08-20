using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SceneInfo
{
    public string SceneName;   // Name of the scene
    public int BuildIndex;     // Optional: Build index of the scene

    public SceneInfo(string sceneName, int buildIndex)
    {
        SceneName = sceneName;
        BuildIndex = buildIndex;
    }
}

public class SceneManager : SingletonPersistent<SceneManager>
{
    [field: SerializeField] public string CurrentSceneName { get; private set; }
    [field: SerializeField] public List<SceneInfo> Scenes { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt("Win", 10);
    }

    public void LoadScene(string sceneName)
    {
        var scene = Scenes.Find(s => s.SceneName == sceneName);
        if (scene != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.SceneName);
            CurrentSceneName = scene.SceneName;
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in the list of scenes.");
        }
    }

    public void ReloadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentSceneName);
    }

    public int GetCurrentScene()
    {
        int currentSceneIndex = Scenes.FindIndex(s => s.SceneName == CurrentSceneName);
        if (currentSceneIndex == -1)
        {
            Debug.LogError("Current scene is not in the list of scenes.");
            return -1;
        }
        return currentSceneIndex;
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = Scenes.FindIndex(s => s.SceneName == CurrentSceneName);
        if (currentSceneIndex == -1)
        {
            Debug.LogError("Current scene is not in the list of scenes.");
            return;
        }

        int nextSceneIndex = (currentSceneIndex + 1) % Scenes.Count;
        LoadScene(Scenes[nextSceneIndex].SceneName);
    }
    public void LoadPreviousScene()
    {
        int currentSceneIndex = Scenes.FindIndex(s => s.SceneName == CurrentSceneName);
        if (currentSceneIndex == -1)
        {
            Debug.LogError("Current scene is not in the list of scenes.");
            return;
        }

        int previousSceneIndex = (currentSceneIndex - 1 + Scenes.Count) % Scenes.Count;
        LoadScene(Scenes[previousSceneIndex].SceneName);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }else if (Input.GetKeyDown(KeyCode.Tab))
        {
            LoadFirstScene();
        }else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadSecondScene();
        }
    }

    private void LoadFirstScene()
    {
        LoadScene(Scenes[0].SceneName);
    }
    private void LoadSecondScene()
    {
        LoadScene(Scenes[1].SceneName);
    }
}
