using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manger : MonoBehaviour
{
    public GameObject[] Cannon_Holders;
    public GameObject[] Cannon_Ball_Holders;
    public GameObject[] BackFire_Holders;
    public GameObject[] BackFireGun_Holders;
    public float CurrentLevel;

    public bool BackFireLock = true;
    #region Singltone
    public static Manger instance;
    void Awake()
    {
        instance = this;
        Cannon_Holders = GameObject.FindGameObjectsWithTag("Cannon_Holder");
        BackFire_Holders = GameObject.FindGameObjectsWithTag("BackFire_Holder");
    }
    #endregion

    
     void Start()
    {
        
        Cannon_Ball_Holders = GameObject.FindGameObjectsWithTag("Cannon_Ball_Holder");
        BackFireGun_Holders = GameObject.FindGameObjectsWithTag("BackFireGun_Holder");
    }

}
