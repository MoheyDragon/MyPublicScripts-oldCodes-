using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Card")]
public class Card_System : ScriptableObject
{
    public new string name;
    public Sprite img;
    public string place_name; public int min_time, max_Time;
    public GameObject[] inputPrefabs,outputPrefabs;
    public Material material;
    public Card_System[] requiredCards;
    //public static bool dragLock = false;
}
