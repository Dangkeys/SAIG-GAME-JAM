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
    private int level = 0;
    void Start()
    {
        levelNameText.text = LevelName;
        if (int.TryParse(LevelName.Split(" ")[1], out int levels))
        {
            level = levels;
        }
        levelButton.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnLevelButtonClicked()
    {
        if (level <= PlayerPrefs.GetInt("Win"))
        {
            SceneManager sceneManager = SceneManager.Instance;
            sceneManager.LoadScene(LevelName);
        }
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
