using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField]
    public int speed;
    public AK.Wwise.Event seaSound,enas,pauseSound,ResumSound,mohey, sailSound;
    public Image black;
    public Material[] pics;
    int countPics,enDcounter;
    int value;
    public GameObject[] islands;
    public Transform startPoint;
    public GameObject []buttons=new GameObject[3];
    public bool isMoving;
    public GameObject finishPic;
    public GameObject[] numbers,DayNight;
    public int[] songs;int songIndex;
    GameObject [] shipSails = new GameObject[6];
    // Start is called before the first frame update
    private void Awake()
    {
        black.gameObject.SetActive(true);
        finishPic.SetActive(false);
        foreach (GameObject item in buttons)
            item.SetActive(false);
        foreach (GameObject item in numbers)
            item.SetActive(false);
        isMoving = true;
        countPics = pics.Length;
        for (int i = 0; i < 6; i++)
             shipSails[i]=transform.GetChild(i).gameObject;
        foreach (GameObject item in shipSails)
            item.SetActive(false);
        shipSails[0].SetActive(true);
    }
    void Start()
    {
        seaSound.Post(gameObject);
        value = 0;enDcounter = 4;
        speed = 0;songIndex = 0;
        StartCoroutine(blackOFF());
    }
    IEnumerator blackOFF()
    {
        enas.Post(gameObject);
        for (float i = 1; i >= 0; i -= Time.deltaTime*0.1f)
            {
                // set color with i as alpha
                black.color = new Color(0, 0, 0, i);
                yield return null;
            }
        foreach (GameObject item in buttons)
            item.SetActive(true);
        buttons[3].SetActive(false);
        numbers[0].SetActive(true);
    }
    public Material ChangePicture()
    {
        if (value==countPics)
        {
            enDcounter--;
            return pics[value - 1];
        }
        else
        {
            value++;
            if (value == songs[songIndex])
                nextSong();
            return pics[value - 1];
        }    
        
    }
    public void speedUP(bool UP)
    {
        if (UP)
        {
            if (speed < 5)
            {
                shipSails[speed].SetActive(false);
                numbers[speed].SetActive(false);
                speed ++;
                numbers[speed].SetActive(true);
                shipSails[speed].SetActive(true);
                sailSound.Post(gameObject); 
            }
        }
        else
        {
            if (speed > 0)
            {
                shipSails[speed].SetActive(false);
                numbers[speed].SetActive(false);
                speed --;
                numbers[speed].SetActive(true);
                shipSails[speed].SetActive(true);
                sailSound.Post(gameObject);
            }
        }
    }
    public void pause(bool isPause)
    {
        if (isPause)
        {
            isMoving = false;
            pauseSound.Post(gameObject);
            buttons[3].SetActive(true);
            buttons[2].SetActive(false);
        }
        else
        {
            isMoving = true;
            ResumSound.Post(gameObject);
            buttons[3].SetActive(false);
            buttons[2].SetActive(true);
        }
    }
    IEnumerator finish()
    {
        speed = 0;
        enas.Stop(gameObject);
        seaSound.Stop(gameObject);
        foreach (GameObject item in buttons)
            item.SetActive(false);
        foreach (GameObject item in numbers)
            item.SetActive(false);
        yield return new WaitForSeconds(7);
        finishPic.SetActive(true);
        mohey.Post(gameObject);
        yield return new WaitForSeconds(10);
        Application.Quit();

    }
    public void SwitchTime()
    {
            DayNight[0].SetActive(false);
            DayNight[1].SetActive(true);
    }
    public void checkforend()
    {
        if (enDcounter == 0)
        {
            StartCoroutine(finish());
        }
    }
    public void nextSong()
    {
        enas.Stop(gameObject);
        enas.Post(gameObject);
        songIndex++;
        if (songIndex == 6)
        {
            songIndex = 0;
            SwitchTime();
        }
    }
}
