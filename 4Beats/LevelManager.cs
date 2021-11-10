using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    const int maxLimit = 25;
    //Positions of the spawners
    Vector3[] EnemySpawners, BulletSpawners;

    //names of the spawners parent
    string Enemy = "EnemySpawners";

    string bull = "BulletSpawners";

    //Bullet
    [SerializeField]
    private GameObject BulletPrefab;
    GameObject[] bullets = new GameObject[maxLimit];
    int bulletIndex;
    //EnemyPooling
    public static GameObject[] enemies= new GameObject[maxLimit];
    public static int enemyIndex;
    public GameObject enemy;
    
    //ParticleSystem Pooling :
    public ParticleSystem enemyHit1, enemyHit2, enemyHit3, DeathParticle, SpawnParticle;
    public static ParticleSystem[] enemyHitPool = new ParticleSystem[maxLimit];
    public static ParticleSystem[] deathPool = new ParticleSystem[maxLimit];
    public static ParticleSystem[] spawnPool = new ParticleSystem[maxLimit];
    public static int hitParticleIndex, DeathParticleIndex, spawnParticleIndex;

    public Level LevelWaves;
    int waveIndex, DirectionIndex;

    public float MainWait;
    bool winLock=false;
    Vector3 rotaiton90 = new Vector3(0,90,0);
    Vector3 rotaiton0 = new Vector3(0, 0, 0);

    public GameObject PlayerHealth;
    int health;
    public int maxHealth;
    void Awake()
    {
        EnemySpawners= new Vector3[4];
        BulletSpawners = new Vector3[4];
        Transform enemeyParent = GameObject.Find(Enemy).transform;
        Transform bullParent = GameObject.Find(bull).transform;
        for (int i = 0; i < 4; i++)
        {
            EnemySpawners[i] = enemeyParent.GetChild(i).transform.position;
            BulletSpawners[i] = bullParent.GetChild(i).transform.position;

            enemies[i] = Instantiate(enemy, transform);
            enemies[i].SetActive(false);

            bullets[i] = Instantiate(BulletPrefab, transform);
            bullets[i].SetActive(false);
            enemyHitPool[i]= Instantiate(hitRandomizer());
            deathPool[i] = Instantiate(DeathParticle, transform);
            spawnPool[i] = Instantiate(SpawnParticle, transform);
            enemyHitPool[i].gameObject.SetActive(false);
            deathPool[i].gameObject.SetActive(false);
            spawnPool[i].gameObject.SetActive(false);
        }
        for (int i = 4; i < maxLimit; i++)
        {
            enemies[i] = Instantiate(enemy, transform);
            enemies[i].SetActive(false);
            bullets[i]= Instantiate(BulletPrefab);
            bullets[i].SetActive(false);
            enemyHitPool[i] = Instantiate(hitRandomizer());
            deathPool[i] = Instantiate(DeathParticle, transform);
            spawnPool[i] = Instantiate(SpawnParticle, transform);
            enemyHitPool[i].gameObject.SetActive(false);
            deathPool[i].gameObject.SetActive(false);
            spawnPool[i].gameObject.SetActive(false);
        }
        bulletIndex = 0;
        enemyIndex = 0;

        DeathParticleIndex = 0;
        hitParticleIndex = 0;
        spawnParticleIndex = 0;

        waveIndex = 0;
        DirectionIndex = 0;
        MainWait = 2;
        winLock = false;
        health = maxHealth;
    }
    private void Update()
    {
        if (winLock)
            return;
        if (Time.time > MainWait)
        {
            spawnOne(0, LevelWaves.Waves[waveIndex].DirectionOrder[DirectionIndex]);
        }
    }

    public void fire(int direction)
    {
        bullets[bulletIndex].transform.position = BulletSpawners[direction];
        bullets[bulletIndex].GetComponent<Collider>().enabled = true;
        if (direction==0)
        bullets[bulletIndex].transform.forward= Vector3.forward;
        else if (direction == 1)
            bullets[bulletIndex].transform.forward = Vector3.right;
        else if (direction == 2)
            bullets[bulletIndex].transform.forward = Vector3.back;
        else
            bullets[bulletIndex].transform.forward = Vector3.left;
        bullets[bulletIndex].SetActive(true);
        bulletIndex++;
        if (bulletIndex == maxLimit)
            bulletIndex = 0;
    }
    ParticleSystem hitRandomizer()
    {
        hitParticleIndex = Random.Range(0, 3);
        if (hitParticleIndex == 0)
            return enemyHit1;
        else if (hitParticleIndex == 1)
            return enemyHit2;
        else
            return enemyHit3;
    }
    void spawnOne(int enemyType,int direction)
    {
        enemyIndex = InstaniteObjects(enemies, enemyIndex, direction);
        spawnParticleIndex = InstaniteObjects(spawnPool, spawnParticleIndex, direction);
        DirectionIndex++;
        MainWait = Time.time + LevelWaves.Waves[waveIndex].spwanRate;
        if (DirectionIndex==LevelWaves.Waves[waveIndex].EnemiesCount)
        {
            DirectionIndex = 0;
            MainWait = Time.time + LevelWaves.Waves[waveIndex].waitAfter;
            waveIndex++;
            if (waveIndex > LevelWaves.Waves.Length - 1)
                win();
        }
    }
    int InstaniteObjects(ParticleSystem[] pool, int index,int direction)
    {
        pool[index].gameObject.transform.position = EnemySpawners[direction];
        if (pool== spawnPool)
        {
            if (direction==1||direction==3)
            {
                pool[index].gameObject.transform.eulerAngles = rotaiton90;
            }
            else
                pool[index].gameObject.transform.eulerAngles = rotaiton0;
        }
        pool[index].gameObject.SetActive(true);
        pool[index].Play();
        index++;
        if (index == 25)
            index = 0;
        return index;
    }
    int InstaniteObjects(GameObject[] pool, int index,int direction)
    {
        
        pool[index].GetComponent<Enemy>().changeForward(EnemySpawners[direction],direction);
        pool[index].gameObject.SetActive(true);
        index++;
        if (index == 25)
            index = 0;
        return index;
    }
    void win()
    {
        Debug.Log("Win");
        winLock = true;
    }
    public void PlayerHit()
    {
        health--;
        if (health == 0)
            Debug.Log("Lose");
    }
}