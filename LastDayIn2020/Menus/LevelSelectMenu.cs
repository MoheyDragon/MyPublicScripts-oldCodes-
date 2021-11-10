using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectMenu : MonoBehaviour
{
    public Transform[] levels;
    public Sprite[] Filers;
    public AK.Wwise.Event click,stopAll;
    public GameObject menu;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (Transform item in levels)
        {
            item.GetChild(0).gameObject.SetActive(true);
        }
    }
    void Start()
    {
        foreach (Transform item in levels)
        {
            item.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < Menu.LevelReached; i++)
        {
            levels[i].Find("Image").GetComponent<Image>().sprite = Filers[i];
            levels[i].Find("Image").GetComponent<Image>().color = Color.white;
            levels[i].GetChild(0).gameObject.SetActive(true);
            levels[i].Find("Locked").gameObject.SetActive(false);
        }
    }
    public void LevelStart()
    {
        if (!EventSystem.current.currentSelectedGameObject.transform.Find("Locked").gameObject.activeSelf)
        {
            click.Post(gameObject);
            stopAll.Post(menu);
            SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex() + 1);
        }
        
    }
}
