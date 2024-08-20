using UnityEngine;

public class PauseManager : SingletonPersistent<PauseManager>
{
    // Start is called before the first frame update
    [field: SerializeField] public GameObject PauseUI;
    [field: SerializeField] public GameObject SettingsUI;
    [field: SerializeField] public GameObject ControlsUI;

    [field: SerializeField] public GameObject PauseButton;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.Instance.GetCurrentScene() > 1)
        {
            TooglePause();
        }
        PauseButton.SetActive(SceneManager.Instance.CurrentSceneName != "MainMenu" && SceneManager.Instance.CurrentSceneName != "SelectScene");
    }
    public void TooglePause()
    {
        if (PauseUI.activeSelf)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }
    public void UnPause()
    {
        PauseUI.SetActive(false);
        SettingsUI.SetActive(false);
        ControlsUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void ShowSettingsUI()
    {
        SettingsUI.SetActive(true);
    }
}
