using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    GameObject Temp;
    public void Return(GameObject hero)
    {
        Temp = hero;
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(1);
        Temp.transform.position = Temp.GetComponent<CharecterController>().CheckPoint;
        Temp.SetActive(true);
        Temp.GetComponent<CharecterController>().Respawn();
    }
}
