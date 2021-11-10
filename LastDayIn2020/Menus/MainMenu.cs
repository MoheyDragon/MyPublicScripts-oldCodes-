using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Lang,MainMenuButtons,LevelSeclectMenu,options,credits,hero,black,JUMPTRY;
    public AK.Wwise.Event Click,stopAll;
    private void Awake()
    {
        Lang.SetActive(true);
        MainMenuButtons.SetActive(true);
        options.SetActive(true);
        credits.SetActive(true);
        LevelSeclectMenu.SetActive(true);
    }
    private void Start()
    {
        JUMPTRY.SetActive(false);
        hero.GetComponent<Animator>().speed = 0.6f;
        MainMenuButtons.SetActive(false);
        Cursor.visible = true;
        DataSave data = SaveSystem.Load();
        if (data!=null)
            StartCoroutine(MenuInstantiate(data.Lang,false));
        else
        {
            black.SetActive(true);
            Menu.LevelReached = 1;
            MainMenuButtons.SetActive(false);
            options.SetActive(false);
            credits.SetActive(false);
            LevelSeclectMenu.SetActive(false);
        }
    }
    public void LangStart(string language)
    {
        LangSet(language);
        StartCoroutine(MenuInstantiate(language, true));
        black.SetActive(false);
        Menu.LevelReached = 1;
        CharecterController.ControlMethode = 1;
        Menu.SFX = 100;
        Menu.MUSIC = 100;
        SaveSystem.Save();
    }
    private void LangGet(string language)
    {
        LangSet(language);
    }
    private void LangSet(string language)
    {
        if (language == "English")
        {
            foreach (GameObject item in Menu.English)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in Menu.Arabic)
            {
                item.SetActive(false);
            }
            Menu.Lang = "English";
        }
        else if (language == "Arabic")
        {
            foreach (GameObject item in Menu.Arabic)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in Menu.English)
            {
                item.SetActive(false);
            }
            Menu.Lang = "Arabic";
        }
    }
    IEnumerator MenuInstantiate(string data,bool start)
    {
        if (!start)
            LangGet(data);
        Lang.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        LevelSeclectMenu.SetActive(false);
        yield return new WaitForSeconds(2);
        MainMenuButtons.SetActive(true);

    }
    public void Continue()
    {
        if (!options.activeSelf)
        {
            Click.Post(gameObject);
            stopAll.Post(gameObject);
            SceneManager.LoadScene(Menu.LevelReached);
        }
            
    }
    public void LevelSelect(bool state)
    {        
        if (!options.activeSelf)
        {
            Click.Post(gameObject);
            LevelSeclectMenu.SetActive(state);
            MainMenuButtons.SetActive(!state);
        }
    }
    public void Options()
    {
        if (!options.activeSelf)
        {
            Click.Post(gameObject);
            options.SetActive(true);
        }
            
            
    }
    public void Back()
    {
        Click.Post(gameObject);
        options.SetActive(false);
        SaveSystem.Save();
    }
    public void Credits(bool state)
    {
        if (!options.activeSelf)
        {
            Click.Post(gameObject);
            black.SetActive(state);
            credits.SetActive(state);
            MainMenuButtons.SetActive(!state);
        }
        
    }
    public void Exit()
    {
        if (!options.activeSelf)
        {
            Click.Post(gameObject);
            Application.Quit();
        }
    }

}
