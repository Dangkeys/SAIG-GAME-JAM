using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPref : MonoBehaviour
{
    
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
