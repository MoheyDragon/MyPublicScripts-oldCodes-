using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Astroid
{
    public GameObject astroidObject;
    public int Count;
    public float startWait, SpawnWait, WaveWait;
}
[System.Serializable]
public class Enemy
{

    public GameObject EnemyObject;
    public int Count;
    public float startWait, SpawnWait, WaveWait;

}

public class Astroid_Spawner_Sc : MonoBehaviour
{

    public Astroid astroid;
    public Enemy enemy;
    public Vector2 SpawnPosition;
    void Start()
    {
        StartCoroutine(AstroidWaveSpwaner());
        StartCoroutine(EnemyWaveSpwaner());
    }
    IEnumerator AstroidWaveSpwaner()
    {
        yield return new WaitForSeconds(astroid.startWait);
        while (true)
        {
            for (int i = 0; i < astroid.Count; i++)
            {
                Vector2 newPos = new Vector2(Random.Range(-SpawnPosition.x, SpawnPosition.x), SpawnPosition.y);
                Instantiate(astroid.astroidObject, newPos, Quaternion.identity);
                yield return new WaitForSeconds(astroid.SpawnWait);

            }
            yield return new WaitForSeconds(astroid.WaveWait);
        }
    }
    IEnumerator EnemyWaveSpwaner()
    {
        yield return new WaitForSeconds(enemy.startWait);
        while (true)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                Vector2 newPos = new Vector2(Random.Range(-SpawnPosition.x, SpawnPosition.x), SpawnPosition.y);
                Instantiate(enemy.EnemyObject, newPos, Quaternion.identity);
                yield return new WaitForSeconds(enemy.SpawnWait);

            }
            yield return new WaitForSeconds(enemy.WaveWait);
        }
    }

}
