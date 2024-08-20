using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]

public class PauseUI : MonoBehaviour
{
    [field: SerializeField] public Button BackButton { get; private set; }
    [field: SerializeField] public Button SettingsButton { get; private set; }
    [field: SerializeField] public Button RestartButton { get; private set; }
    [field: SerializeField] public Button MainMenuButton { get; private set; }
    [field: SerializeField] public GameObject ControlsUI { get; private set; } 
    [field: SerializeField] public Button ControlsButton { get; private set; }
    [field: SerializeField][SceneObjectsOnly] public GameObject SettingsUI;
    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(OnBackButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        RestartButton.onClick.AddListener(OnRestartButtonClicked);
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        ControlsButton.onClick.AddListener(OnControlsButtonClicked);
    }

    private void OnControlsButtonClicked()
    {
        ControlsUI.TryGetComponent(out IUIElement uiElement);
        uiElement.Show();
    }

    private void OnEnable()
    {
        if(SceneManager.Instance == null) return;
        MainMenuButton.gameObject.SetActive(SceneManager.Instance.CurrentSceneName != "MainMenu");
        RestartButton.gameObject.SetActive(SceneManager.Instance.CurrentSceneName != "MainMenu" && SceneManager.Instance.CurrentSceneName != "SelectScene");
        
        ControlsButton.gameObject.SetActive(SceneManager.Instance.CurrentSceneName != "MainMenu");
    }

    private void OnMainMenuButtonClicked()
    {
        PauseManager.Instance.UnPause();
        SceneManager.Instance.LoadScene("MainMenu");
    }

    private void OnRestartButtonClicked()
    {
        PauseManager.Instance.UnPause();
        SceneManager.Instance.ReloadCurrentScene();
    }

    private void OnSettingsButtonClicked()
    {
        SettingsUI.SetActive(true);
    }

    private void OnBackButtonClicked()
    {
        PauseManager.Instance.TooglePause();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {
        BackButton.onClick.RemoveListener(OnBackButtonClicked);
        SettingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        RestartButton.onClick.RemoveListener(OnRestartButtonClicked);
        MainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        ControlsButton.onClick.RemoveListener(OnControlsButtonClicked);
    }
}
