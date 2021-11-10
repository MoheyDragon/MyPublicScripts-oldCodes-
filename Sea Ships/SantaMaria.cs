using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SantaMaria : PlayerShip
{
    public GameObject Cannons, Cannonball, BackFireHolders, BackFireBullet, Turret, TurretBullet;
    Transform TurretBulletHolder;
    bool TurretLock = false; float fireRate = 0.2f; float nextFire = 0f; public float TurretAmmo;
    public int Cannons_Unlocked = 2;
    public bool BackFireUnlocked = true;
    public bool TurretUnlocked = true;
    public Image CannonLoadingbar, TurretLoadingbar;
    public float CannonLoad_Speed, TurretLoad_Speed;
    public GameObject[] FallHolders;

    public override void Awake()
    {
        base.Awake();
        int count = 1;
        foreach (GameObject Holder in Manger.instance.Cannon_Holders)
        {
            if (count <= Cannons_Unlocked)
            {
                Instantiate(Cannons, Holder.transform);
                count++;
            }

        }
        count = 1;
        if (BackFireUnlocked)
        { foreach (GameObject Holder in Manger.instance.BackFire_Holders) { if (count <= 4) { Instantiate(BackFireHolders, Holder.transform); count++; } } }
        if (TurretUnlocked)
        {
            Instantiate(Turret, GameObject.FindGameObjectWithTag("TurretHolder").transform);
            TurretBulletHolder = GameObject.FindGameObjectWithTag("TurretBulletHolder").transform;
        }
    }
    void FixedUpdate()
    {
        if (CannonLoadingbar.fillAmount <= 1)   CannonLoadingbar.fillAmount += 0.25f * CannonLoad_Speed * Time.deltaTime;
        if (TurretLoadingbar.fillAmount<=1&&!Input.GetKey(KeyCode.Q))  TurretLoadingbar.fillAmount += 0.1f * TurretLoad_Speed * Time.deltaTime;
        if (TurretLoadingbar.fillAmount<=0.05f) TurretLock = true;
        if (TurretLoadingbar.fillAmount == 1)TurretLock = false;
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))  Raming(0);
        if (Input.GetMouseButtonDown(1))  Raming(1);
        if (Input.GetKeyDown(KeyCode.Space) && RamLock == false && CannonLoadingbar.fillAmount == 1)
        {
            foreach (GameObject Holder in Manger.instance.Cannon_Ball_Holders)
            {
                Instantiate(Cannonball, Holder.transform.position, Holder.transform.rotation);
            }
            CannonLoadingbar.fillAmount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Z) && RamLock == false)
        {
            foreach (GameObject Holder in Manger.instance.BackFireGun_Holders)
            {
                Instantiate(BackFireBullet, Holder.transform.position, Holder.transform.rotation);
            }
        }
        if (Input.GetKey(KeyCode.Q) && RamLock == false && TurretLock == false)
        {
            if (Time.time > nextFire)
            {
                Instantiate(TurretBullet, TurretBulletHolder.position, TurretBulletHolder.rotation);
                nextFire = Time.time + fireRate;
                TurretLoadingbar.fillAmount -= 1/TurretAmmo;
            }
        }
    }
    void UnlockRaming()
    {
        Anime.SetInteger("RamDir", 2);
        RamLock = false;
        WaveInstantiate();
    }
    void Raming(int Direction)
    {

        RamLock = true;
        Anime.SetInteger("RamDir", Direction);
        WavesParticle.loop = false;
        WavesHolder.DetachChildren();

    }
    public void CrewFell(int Number,Transform TransPos)
    {
        float Near = 0;
        Transform Holder = transform;
        for (int i = 0; i < FallHolders.Length; i++)
        {
            float Distance = Vector3.Distance(TransPos.position, FallHolders[i].transform.position);
            if (Distance > Near)
            {
                Holder = FallHolders[i].transform;
                Near = Distance;
            }
        }
        CrewNumber -= Number;
        Instantiate(CrewFellAnimation,Holder);
    }



}
