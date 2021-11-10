using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuldingEnemies : MonoBehaviour
{
    public int NumberOfEnemies=1;
    public GameObject Zombie;
    public Transform[] Zombies_Spawners;
    private void Start()
    {

        for (int i = 0; i < NumberOfEnemies; i++)
            Instantiate(Zombie, Zombies_Spawners[i].position, Quaternion.identity, transform);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {

            foreach (Transform child in transform)
            {
                if (child.tag == "Enemy")
                {
                    if (!child.GetComponent<EnemyAi>().Lock)
                    child.GetComponent<EnemyAi>().Chase(true);
                }

            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Enemy")
                {
                    child.GetComponent<EnemyAi>().Chase(false);
                }
            }
        }
    }
}
