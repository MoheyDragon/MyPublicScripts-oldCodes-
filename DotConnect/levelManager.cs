using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class levelManager : MonoBehaviour
{
    [HideInInspector]
    public ConnectChecker[] Checkers;
    public float strength=30;
    public int vibrate = 5;
    GameObject FinalPic;
    float MainTime;
    float waitTime=5;
    Transform NextShrink;
    bool firstShrink,Win;
    public GameObject magicParticleSystem,smoke;
    void Start()
    {
        MainTime = waitTime;
        firstShrink = true;
        Win = false;
        Checkers = new ConnectChecker[transform.childCount-1];
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Checkers[i] = new ConnectChecker(new bool[transform.GetChild(i).childCount], transform.GetChild(i).childCount);
        }
        FinalPic = transform.GetChild(transform.childCount-1).gameObject;
        FinalPic.SetActive(false);
    }
    public bool ColorFinishedCheck(int connect,int type)
    {
        Checkers[type].DotsConnected[connect] = true;
        foreach (bool i in Checkers[type].DotsConnected)
        {
            if (i==false)
                return false;
        }
        Checkers[type].Finished = true;
        return LevelFinished(true);
    }
    public bool LevelFinished(bool check)
    {
        for (int i = 0; i < Checkers.Length; i++)
        {
            if (!Checkers[i].Finished)
                return false;
        }
        return true;
    }
    public void NextCheckIn(int _id,int type,bool isAlt)
    {
        firstShrink = false;
        if (Checkers[type].firstMove)
        {
            Checkers[type].firstMove = false;
            Checkers[type].currentId = _id + 1;
            if (_id+1==Checkers[type].DotsCount)
                Checkers[type].currentId = 0;
            Checkers[type].altCurrentId = _id;
        }
        if (ColorFinishedCheck(_id,type))
        {
            StartCoroutine(WinFunction());
            return;
        }
        if (Checkers[type].Finished)
            return;
        if (isAlt)
        {
            Checkers[type].altCurrentId = _id;
            if (_id == 0)
            {
                NextShrink = transform.GetChild(type).GetChild(Checkers[type].DotsCount-1);
                MainTime = Time.time + waitTime;
                Shrink();
            }
            else
            {
                NextShrink = transform.GetChild(type).GetChild(_id -1);
                MainTime = Time.time + waitTime;
                Shrink();
            }

        }
        else
        {
            Checkers[type].currentId = _id + 1;
            if (Checkers[type].currentId == Checkers[type].DotsCount)
                Checkers[type].currentId = 0;
            if (_id == Checkers[type].DotsCount - 2)
            {
                NextShrink = transform.GetChild(type).GetChild(0);
                MainTime = Time.time + waitTime;
                Shrink();
            }
            else if (_id == Checkers[type].DotsCount - 1)
            {
                NextShrink = transform.GetChild(type).GetChild(1);
                MainTime = Time.time + waitTime;
                Shrink();
            }
            else
            {
                NextShrink = transform.GetChild(type).GetChild(_id + 2);
                MainTime = Time.time + waitTime;
                Shrink();
            }
        }
    }
    private void Update()
    {
        if (Time.time>=MainTime)
        {
            MainTime += waitTime;
            Shrink();
        }
    }
    public void Shrink()
    {
        if (!Win)
        if (!firstShrink)
        NextShrink.DOShakeScale(1.5f, strength, vibrate);
    }
    IEnumerator WinFunction()
    {
        for (int i = 0; i < Checkers.Length; i++)
        {
            for (int j = 0; j < Checkers[i].DotsCount; j++)
                Instantiate(magicParticleSystem, transform.GetChild(i).GetChild(j).position, Quaternion.identity);
            transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.4f);
        Instantiate(smoke, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        FinalPic.SetActive(true);
        FinalPic.transform.DOScaleX(1.5f, 1);
        FinalPic.transform.DOScaleY(1.2f, 1);
        yield return new WaitForSeconds(1);
        FinalPic.transform.DOScaleY(1, 0.75f);
        FinalPic.transform.DOScaleX(1, 0.75f);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
public class ConnectChecker
{
    public bool[] DotsConnected;
    public int DotsCount;
    public int currentId;
    public int altCurrentId;
    public bool firstMove;
    public bool Finished;

    public ConnectChecker(bool[] connecters, int dotsCount)
    {
        this.DotsConnected = connecters;
        this.DotsCount = dotsCount;
        firstMove = true;
        Finished = false;
        currentId = -10;
        altCurrentId = -10;
    }
}

