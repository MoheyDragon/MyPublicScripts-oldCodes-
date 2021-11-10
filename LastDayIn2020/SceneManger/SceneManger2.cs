using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger2 : MonoBehaviour
{
    GameObject Hero;
    public GameObject Black, Green, Green2, JumpImage, PowerImage,TriesImage,
        T1, T2, T3, T4,T5,T6,T7,T8,T9,T10,
        S1, S2, S3, S4, S5,S6,S7, S8,S9,S10,S11,S12,
        House4, House5, House6,House7,House8, House9, House10, 
        CheckPoint1, CheckPoint2, CheckPoint3, CheckPoint4, CheckPoint5, CheckPoint6, CheckPoint7, CheckPoint8;
    public static int CubsCheck; bool CPl1, CPl2, CPl3, CPl4,CPl5, CPl6, CPl7, CPl8,cubsLock;
    int scene;
    public AK.Wwise.Event stopAll;
    GameObject[] Houses;
    private void Awake()
    {
        T1.SetActive(true); T2.SetActive(true); T3.SetActive(true); T4.SetActive(true); T5.SetActive(true); T6.SetActive(true); T7.SetActive(true); T8.SetActive(true); T9.SetActive(true); T10.SetActive(true);
        Green.SetActive(true);Green2.SetActive(true);S1.SetActive(true); S2.SetActive(true); S3.SetActive(true); S4.SetActive(true); S5.SetActive(true); S6.SetActive(true); S7.SetActive(true); S8.SetActive(true); S9.SetActive(true); S10.SetActive(true); S11.SetActive(true); S12.SetActive(true);
    }
    private void Start()
    {
        JumpImage.SetActive(false); PowerImage.SetActive(false); TriesImage.SetActive(false);
        T1.SetActive(false); T2.SetActive(false); T3.SetActive(false); T4.SetActive(false); T5.SetActive(false); T6.SetActive(false); T7.SetActive(false); T8.SetActive(false); T9.SetActive(false); T10.SetActive(false);
        Green2.SetActive(false); S2.SetActive(false); S3.SetActive(false); S4.SetActive(false); S5.SetActive(false); S6.SetActive(false); S7.SetActive(false); S8.SetActive(false); S9.SetActive(false); S10.SetActive(false); S11.SetActive(false); S12.SetActive(false);
        CPl1 =  CPl2 = CPl3 =  CPl4 = CPl5 = CPl6 = CPl7 = CPl8=cubsLock = false;
        Camera.main.GetComponent<Camera_Controller>().FollowPlayer();
        Hero = GameObject.FindGameObjectWithTag("Player");
        CubsCheck = 0;
        CharecterController.Level = 0;
        JumpImage.SetActive(false); PowerImage.SetActive(false);TriesImage.SetActive(false);
    }
    void Update()
    {
        if (CheckPoint1==null&&!CPl1)
        {
          CPl1 = true;
            S1.SetActive(false); Green.SetActive(false);
            StartCoroutine(H1());
            
        }
        if (scene==1&&Input.GetKeyDown(KeyCode.Alpha1))
        {
            scene = 2;
            S2.SetActive(false);S3.SetActive(true); House4.SetActive(true);
        }
        if (CheckPoint2 == null && !CPl2)
        {
            CPl2 = true;
            CharecterController.Level = 2;
            S3.SetActive(false); S4.SetActive(true);scene = 3;
        }
        if (scene==3&&Input.GetKeyDown(KeyCode.Alpha2))
        {
            scene = 4;
            S4.SetActive(false);S5.SetActive(true);House5.SetActive(true);
        }
        if (CheckPoint3 == null && !CPl3)
        {
            CPl3 = true;
            S5.SetActive(false);Green.SetActive(false);
            StartCoroutine(H2());
        }
        if (CubsCheck==3&&!cubsLock)
        {
            cubsLock = true;
            StartCoroutine(H3());          
        }
        if (CheckPoint4==null&&!CPl4)
        {
            CPl4 = true;
            House7.SetActive(true);S8.SetActive(false);S9.SetActive(true); Green.SetActive(false); Green2.SetActive(true);
        }
        if (CheckPoint5==null&&!CPl5)
        {
            CPl5 = true;
            Green2.SetActive(false); Green.SetActive(true);
            S9.SetActive(false);S10.SetActive(true);House8.SetActive(true);
        }
        if (CheckPoint6 == null && !CPl6)
        {
            CPl6 = true;
            S10.SetActive(false); S11.SetActive(true); House9.SetActive(true);
        }
        if (CheckPoint7 == null && !CPl7)
        {
            CPl7 = true;
            S11.SetActive(false); S12.SetActive(true); House10.SetActive(true);
        }
        if (CheckPoint8 == null && !CPl8)
        {
            CPl8 = true;
            Green.SetActive(false); S12.SetActive(false);
            StartCoroutine(H4());
        }


    }
    IEnumerator H1()
    {
        yield return new WaitForSeconds(2);
        stopAll.Post(Hero);
        Black.SetActive(true); Hero.SetActive(false);
        yield return new WaitForSeconds(2);
        T1.SetActive(true);
        yield return new WaitForSeconds(5);
        Black.SetActive(false); T1.SetActive(false); Hero.SetActive(true);
        S2.SetActive(true); Green.SetActive(true);
        CharecterController.Level = 1;
        scene = 1;
    }
    IEnumerator H2()
    {
        yield return new WaitForSeconds(2);
        stopAll.Post(Hero);
        Black.SetActive(true); Hero.SetActive(false);
        yield return new WaitForSeconds(2);
        T2.SetActive(true);
        yield return new WaitForSeconds(10);
        T3.SetActive(true);
        yield return new WaitForSeconds(3);
        T4.SetActive(true);
        yield return new WaitForSeconds(10);
        CharecterController.Level = 4;
        Black.SetActive(false); T2.SetActive(false); T3.SetActive(false); T4.SetActive(false); Hero.SetActive(true);
        S6.SetActive(true); Green2.SetActive(true); JumpImage.SetActive(true); PowerImage.SetActive(true);
        yield return new WaitForSeconds(7);
        S6.SetActive(false); S7.SetActive(true); House6.SetActive(true);
        yield return new WaitForSeconds(10);
        S7.SetActive(false); Green2.SetActive(false);
    }
    IEnumerator H3()
    {
        S7.SetActive(false); Green2.SetActive(false); 
        yield return new WaitForSeconds(2);
        stopAll.Post(Hero);
        Black.SetActive(true); JumpImage.SetActive(false); PowerImage.SetActive(false); Hero.SetActive(false);
        yield return new WaitForSeconds(2);
        T5.SetActive(true);
        yield return new WaitForSeconds(7); 
        Black.SetActive(false);Green.SetActive(true);T5.SetActive(false); Hero.SetActive(true);
        S8.SetActive(true); CheckPoint4.SetActive(true); JumpImage.SetActive(true); PowerImage.SetActive(true);
    }
    IEnumerator H4()
    {
        yield return new WaitForSeconds(2);
        stopAll.Post(Hero);
        foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
        {
            try
            {
                stopAll.Post(game);
                game.GetComponent<BuldingUpDown>().speed = 0;
            }
            catch (System.Exception)
            {
                break;
            }
            
        }
        Black.SetActive(true);JumpImage.SetActive(false);PowerImage.SetActive(false);Hero.SetActive(false);
        yield return new WaitForSeconds(2);
        T6.SetActive(true);
        yield return new WaitForSeconds(7);
        T7.SetActive(true); TriesImage.SetActive(true);
        yield return new WaitForSeconds(5);
        T8.SetActive(true); TriesImage.SetActive(true);
        yield return new WaitForSeconds(5);
        T9.SetActive(true);
        if (Menu.LevelReached == 2)
        {
            Menu.LevelReached = 4;
            SaveSystem.Save();
        }
        T10.SetActive(true);
        Cursor.visible = true;
    }
    public void LevelChoose(int level)
    {
        stopAll.Post(gameObject);
        SceneManager.LoadScene(level);
    }
}
