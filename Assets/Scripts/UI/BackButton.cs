using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    // Start is called before the first frame update
    Button backButton;
    private void Awake() {
        backButton = GetComponent<Button>();
    }
    void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnBackButtonClicked()
    {
        SceneManager sceneManager = SceneManager.Instance;
        sceneManager.LoadPreviousScene();
    }
    void OnDestroy()
    {
        backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
