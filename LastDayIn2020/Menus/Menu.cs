using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [HideInInspector]
    public GameObject hero;
    public GameObject MenuScreen,options,GameOverScreen,EndCredits;
    public static bool Pause = false;
    public static string Lang;
    public static int LevelReached;
    [HideInInspector]
    public static GameObject[] Arabic, English;
    public AK.Wwise.Event Click,PauseALL,ResumeALL,MenuO_C,MusicStart,stopAll;
    public AK.Wwise.RTPC Music, Sfx;
    public AK.Wwise.Switch Level;
    public Slider SoundEffects, music;
    public static float MUSIC, SFX;
    DataSave data;
    // Start is called before the first frame update
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            MenuScreen.SetActive(true);
            options.SetActive(true);
            GameOverScreen.SetActive(true);
        }
        if (SceneManager.GetActiveScene().buildIndex==4)
        {
            EndCredits = GameObject.FindGameObjectWithTag("EndCredits");
        }
        Arabic = GameObject.FindGameObjectsWithTag("Arabic");
        English = GameObject.FindGameObjectsWithTag("English");
        hero = GameObject.FindGameObjectWithTag("Player");
        Hover_Click_Sounds.button = null;
        Hover_Click_Sounds.look = false;
        Level.SetValue(gameObject);
        data = SaveSystem.Load();
        if (data != null)
        {
            SFX = data.SFX; MUSIC = data.Music;
            Sfx.SetGlobalValue(SFX);
            SoundEffects.value = data.SFX/100;
            Music.SetGlobalValue(MUSIC);
            music.value = data.Music/100;
        }
    }
    private void Start()
    {
        if (data != null)
        {
            Lang = data.Lang;
            LevelReached = data.Level;
            CharecterController.ControlMethode = data.Methode;
        }
        if (SceneManager.GetActiveScene().name!= "MainMenu")
        {
            Cursor.visible = false;
            MenuScreen.SetActive(false);
            options.SetActive(false);
            GameOverScreen.SetActive(false);
            LangSet(data.Lang);
        }
        MusicStart.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name!="MainMenu")
            {
                if (SceneManager.GetActiveScene().buildIndex!=4)
                {
                    if (options.activeSelf)
                        Options(false);
                    else
                        Resume(MenuScreen.activeSelf);
                }
                else
                {
                    if (!GameOverScreen.activeSelf&&!EndCredits.activeSelf)
                    {
                        if (options.activeSelf)
                            Options(false);
                        else
                            Resume(MenuScreen.activeSelf);
                    }
                }
                
            }
        }
    }
    public void Resume(bool State)
    {
        if (State)
        {
            foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
            {
                ResumeALL.Post(game);
            }
            ResumeALL.Post(hero);
            ResumeALL.Post(gameObject);
        }
        else
        {
            foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
            {
                PauseALL.Post(game);
            }
            PauseALL.Post(hero);
            PauseALL.Post(gameObject);
        }
            
        MenuScreen.SetActive(!State);
        Pause = !State;
        Time.timeScale = Pause ? 0 : 1;
        hero.GetComponent<CharecterController>().Pause(!State);
        Camera.main.GetComponent<Animator>().speed = State ? 1 : 0;
        Cursor.visible = !State;
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (!SceneManger3.SkipLock)
                Camera_Controller.CameraSkip.SetActive(State);
        }
        MenuO_C.Post(gameObject);
    }
    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Pause = true;
        hero.GetComponent<CharecterController>().Pause(true);
        Cursor.visible = true;
    }
    public void Restart()
    {
        Click.Post(gameObject);
        foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
        {
            stopAll.Post(game);
        }
        stopAll.Post(hero);
        stopAll.Post(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume(true);
    }
    public void Options(bool T)
    {
        Click.Post(gameObject);
        options.SetActive(T);
        MenuScreen.SetActive(!T);
        if (!T)
            SaveSystem.Save();
    }
    public void LangChange()
    {
        if (Lang=="English")
        {
            foreach (GameObject item in Arabic)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in English)
            {
                item.SetActive(false);
            }
            Lang = "Arabic";
        }
        else if (Lang=="Arabic")
        {
            foreach (GameObject item in English)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in Arabic)
            {
                item.SetActive(false);
            }
            Lang = "English";
        }
        Click.Post(gameObject);
        SaveSystem.Save();
        
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
            Lang = "English";
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
            Lang = "Arabic";
        }
    }

    public void MainMenu()
    {
        Click.Post(gameObject);
        foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
        {
            stopAll.Post(game);
        }
        stopAll.Post(gameObject);
        Time.timeScale = 1;
        CharecterController.cutscene = false;
        Pause = false;
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Click.Post(gameObject);
        Application.Quit();
    }
    public void slider()
    {
        Sfx.SetGlobalValue(SoundEffects.value*100);
        Music.SetGlobalValue(music.value*100);
        SFX = SoundEffects.value*100;
        MUSIC = music.value*100;
    }
}
