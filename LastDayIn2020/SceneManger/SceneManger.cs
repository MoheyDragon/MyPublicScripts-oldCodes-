using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    public GameObject T1, T2, T3,T4,T5,T6,T7 ,Black,Green,Green2,S1,S2,S3,S4,S5,S6,JumpTryPOWER; Camera Cam;
    GameObject hero;
    int Scene = 0; bool Wcheck, Scheck, Acheck, Dcheck; float ZoomCheck;
    public AK.Wwise.Event stopALL;
    private void Awake()
    {
        T1.SetActive(true); T2.SetActive(true); T3.SetActive(true); T4.SetActive(true); T5.SetActive(true); T6.SetActive(true); T7.SetActive(true);
        Green.SetActive(true); Green2.SetActive(true); S1.SetActive(true); S2.SetActive(true); S3.SetActive(true); S4.SetActive(true); S5.SetActive(true); S6.SetActive(true);
    }
    private void Start()
    {
        JumpTryPOWER.SetActive(false);
        T1.SetActive(false); T2.SetActive(false); T3.SetActive(false); T4.SetActive(false); T5.SetActive(false); T6.SetActive(false); T7.SetActive(false);
        Green.SetActive(false); Green2.SetActive(false);S1.SetActive(false); S2.SetActive(false); S3.SetActive(false); S4.SetActive(false); S5.SetActive(false); S6.SetActive(false);
        Cam = Camera.main;
        CharecterController.cutscene = true;
        CharecterController.Level = 0;
        hero = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(Sequnce());
    }
    private void Update()
    {
        if (Scene==1)
        {
            if (Input.GetKeyDown(KeyCode.W)) Wcheck = true;
            if (Input.GetKeyDown(KeyCode.S)) Scheck = true;
            if (Wcheck && Scheck)
            {
                Scene = 2; S1.SetActive(false);S2.SetActive(true);
            }
        }
        if (Scene==2)
        {
            if (Input.GetKeyDown(KeyCode.A)) Acheck = true;
            if (Input.GetKeyDown(KeyCode.D)) Dcheck = true;
            if (Acheck && Dcheck)
            {
                Scene = 3; S2.SetActive(false); S3.SetActive(true);
                Acheck = Dcheck = false;
                ZoomCheck = Camera_Controller.CurrentZoom;
            }
        }
        if (Scene == 3 && Camera_Controller.CurrentZoom != ZoomCheck)
        { Scene = 4; S3.SetActive(false); S4.SetActive(true); Green.SetActive(false); Green2.SetActive(true); }
        if (Scene==4)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) 
            {
                Scene = 5; S4.SetActive(false);S5.SetActive(true);Green2.SetActive(false);Green.SetActive(true);
            }
        }
        if (Scene==5)
        {
            if (Input.GetKeyDown(KeyCode.A)) Acheck = true;
            if (Input.GetKeyDown(KeyCode.D)) Dcheck = true;
            if (Acheck && Dcheck)
            {
                Scene = 6; S5.SetActive(false); S6.SetActive(true);
            }
        }


        if (Scene == 6 && Input.GetKeyDown(KeyCode.Space))
        { Scene = 0; S6.SetActive(false); Green.SetActive(false); StartCoroutine(Sequnce2()); }

    }

    IEnumerator Sequnce()
    {
        yield return new WaitForSeconds(2);
        T1.SetActive(true);
        yield return new WaitForSeconds(7);
        T2.SetActive(true);
        yield return new WaitForSeconds(10);
        T3.SetActive(true);
        yield return new WaitForSeconds(7);
        T4.SetActive(true);
        yield return new WaitForSeconds(7);
        T1.SetActive(false); T2.SetActive(false); T3.SetActive(false); T4.SetActive(false); Black.SetActive(false);
        S1.SetActive(true); Green.SetActive(true);
        Cam.GetComponent<Camera_Controller>().FollowPlayer(); CharecterController.cutscene=false;
        Scene = 1;

    }
    IEnumerator Sequnce2()
    {
        yield return new WaitForSeconds(3);
        stopALL.Post(hero);
        hero.SetActive(false);
        CharecterController.cutscene = true;
        Black.SetActive(true);
        T5.SetActive(true);
        yield return new WaitForSeconds(5);
        T6.SetActive(true);
        yield return new WaitForSeconds(4);
        T7.SetActive(true);
        yield return new WaitForSeconds(5);
        if (Menu.LevelReached==1)
        {
            Menu.LevelReached = 2;
            SaveSystem.Save();
        }
        stopALL.Post(gameObject);
        SceneManager.LoadScene("Level2");


    }

}
