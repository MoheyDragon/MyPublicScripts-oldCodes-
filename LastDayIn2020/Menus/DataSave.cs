using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSave
{
    public int Level;
    public string Lang;
    public int Methode;
    public float Music, SFX;
    public DataSave()
    {
        Lang = Menu.Lang;
        Level = Menu.LevelReached;
        Methode = CharecterController.ControlMethode;
        Music = Menu.MUSIC;
        SFX = Menu.SFX;
    }
}
