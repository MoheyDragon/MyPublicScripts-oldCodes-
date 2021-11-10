using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    GameObject start,cont;
    private void Awake()
    {
        start = transform.GetChild(0).gameObject;
        cont = transform.GetChild(1).gameObject;
        start.SetActive(true);
        cont.SetActive(true);
    }
    void Start()
    {
        if (Menu.LevelReached > 1)
        {
            cont.SetActive(true);
            start.SetActive(false);
        }
        else
        {
            cont.SetActive(false);
            start.SetActive(true);
        }
    }
}
