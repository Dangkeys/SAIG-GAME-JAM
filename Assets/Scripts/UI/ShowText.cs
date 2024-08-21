using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    private TMP_Text showText;
    [SerializeField] private List<string> text;
    private int indexText = 0;
    void Start()
    {
        showText = GetComponent<TMP_Text>();
        showText.text = text[indexText];
    }

    public void SetIndexText(int number)
    {
        indexText = number;
        showText.text = text[indexText];
    }
}
