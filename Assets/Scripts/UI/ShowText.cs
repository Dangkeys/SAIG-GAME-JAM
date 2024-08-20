using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    [SerializeField] private TMP_Text showText;
    [SerializeField] private List<string> text;
    private int indexText = 0;
    void Start()
    {
        showText.text = text[indexText];
    }

    public void SetIndexText(int number)
    {
        indexText = number;
        showText.text = text[indexText];
    }
}
