using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorForSailors : MonoBehaviour
{
    public GameObject Sailor, Crew;
    public int conuter = 0;
    bool SpwanLock = false;
    // Start is called before the first frame update
    void Update()
    {
        if (conuter <= PlayerShip.CrewNumber/5 - 1 && SpwanLock == false)
        {
            StartCoroutine(Spawn());
        }

    }
    IEnumerator Spawn()
    {
        SpwanLock = true;
        Instantiate(Sailor, transform.position, Quaternion.identity, Crew.transform);
        conuter++;
        yield return new WaitForSeconds(2);
        SpwanLock = false;
    }
}
