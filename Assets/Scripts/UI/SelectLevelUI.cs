using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    [field: SerializeField] public Transform levelUIParent { get; private set; }
    [field: SerializeField] public List<GameObject> levelUIPrefab { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

        CreateLevelUIElements();
    }

    private void OnLevelButtonClicked()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void CreateLevelUIElements()
    {
        int scene = 1;
        int current = PlayerPrefs.GetInt("Win");
        SceneManager sceneManager = SceneManager.Instance;
        foreach (SceneInfo sceneInfo in sceneManager.Scenes)
        {
            if (sceneInfo.BuildIndex == -1 || sceneInfo.BuildIndex == 0 || sceneInfo.BuildIndex == 1)
            {
                continue;
            }
            GameObject levelUI;
            if (scene <= current)
            {
                levelUI = Instantiate(levelUIPrefab[0], levelUIParent);
            }
            else
            {
                levelUI = Instantiate(levelUIPrefab[1], levelUIParent);
            }
            LevelUIElement levelUIElement = levelUI.GetComponent<LevelUIElement>();
            levelUIElement.SetLevelName(sceneInfo.SceneName);
            scene++;
            
        }
    }
}
