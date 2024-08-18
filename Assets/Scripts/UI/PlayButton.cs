using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    Button playButton;
    private void Awake() {
        playButton = GetComponent<Button>();
    }
    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        SceneManager sceneManager = SceneManager.Instance;
        sceneManager.LoadNextScene();
    }
    void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
