using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger3 : MonoBehaviour
{
    public static bool VictoryCheck;bool VicLock = false;  public GameObject black, Credits;
    [HideInInspector]
    public GameObject hero;
    public static bool SkipLock;bool WinLock;
    public AK.Wwise.Event stopAll;
    private void Awake()
    {
        Credits.SetActive(true);
        WinLock = true;
        hero = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        Credits.SetActive(false);
        VictoryCheck = false;
        SkipLock = false;
        CharecterController.Level = 5;
        CharecterController.cutscene = true;
    }
    private void Update()
    {
        if (VictoryCheck&&!VicLock)
        {
            VicLock = true;
            StartCoroutine(Win());
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!WinLock)
            {
                WinLock = true;
                Application.Quit();
            }
        }
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
        {
            stopAll.Post(game);
            game.GetComponent<BuldingUpDown>().speed = 0;
        }
        black.SetActive(true); Credits.SetActive(true);
        hero.GetComponent<CharecterController>().CutScene(true);
        hero.SetActive(false);
        WinLock = false;
    }

}
