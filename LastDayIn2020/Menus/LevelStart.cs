using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    public void Click()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex());
        //SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex()+1);
    }
}
