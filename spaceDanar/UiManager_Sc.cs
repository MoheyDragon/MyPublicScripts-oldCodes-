using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UiManager_Sc : MonoBehaviour
{
    public Image BackLaser, BackAstroid;
    public static UiManager_Sc Instance;
    public GameObject PanelGameOver;
    public Text Score;
    public Text FinalScore;
    void Awake()
    {
        Instance = this;
    }
}
