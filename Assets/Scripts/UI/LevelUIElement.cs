using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIElement : MonoBehaviour
{
    [field: SerializeField] public string LevelName { get; private set; }
    [field: SerializeField] public Button levelButton { get; private set; }
    [SerializeField]  TextMeshProUGUI levelNameText;
    void Start()
    {
        levelNameText.text = LevelName;
        levelButton.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnLevelButtonClicked()
    {
        SceneManager sceneManager = SceneManager.Instance;
        sceneManager.LoadScene(LevelName);
    }

    public void SetLevelName(string levelName)
    {
        LevelName = levelName;
        levelNameText.text = LevelName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        levelButton.onClick.RemoveListener(OnLevelButtonClicked);
    }
}
