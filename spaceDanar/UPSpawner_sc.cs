using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPSpawner_sc : MonoBehaviour
{
    public List<GameObject> Powers;
    [HideInInspector]
    public static GameObject lastPower;

    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            int randomIndex = Random.Range(0, Powers.Count);
            var power = Powers[randomIndex];
            lastPower = Instantiate(power, Vector3.zero, Quaternion.identity);
            yield return new WaitForSeconds(10);
            if (lastPower != null)
                Destroy(lastPower);
            yield return new WaitForSeconds(20);
        }
    }
}






