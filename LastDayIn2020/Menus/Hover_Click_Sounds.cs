using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover_Click_Sounds : MonoBehaviour
{
    public AK.Wwise.Event Hover;
    public static bool look = false;
    public static GameObject button;
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && !look)
        {
            Hover.Post(gameObject);
            look = true;
            button = gameObject;
        }
        if (!EventSystem.current.IsPointerOverGameObject()&&button==gameObject)
        {
            look = false;
        }
    }
}
