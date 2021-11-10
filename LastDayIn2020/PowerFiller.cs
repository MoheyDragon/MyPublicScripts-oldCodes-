using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFiller : MonoBehaviour
{
    bool Lock = false;
    public int Value; Collider coll;
    
    private void Start()
    {
        coll = this.GetComponent<Collider>();
    }
    void Update()
    {
        if (!Menu.Pause)
            transform.Rotate(new Vector3(0, 1, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"&&!Lock)
        {
            coll.enabled= false;
            Lock = true;
            other.GetComponent<CharecterController>().PowerUsage(Value);
            if (CharecterController.Level==4)
                SceneManger2.CubsCheck++;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }
    public void  Rebirth()
    {
        if (Lock)
        {
            foreach (Transform child in transform)
            {
                coll.enabled = true;
                child.gameObject.SetActive(true);
                Lock = false;
            }
        }
    }
    public void Destroy ()
    {
        if (Lock)
        {
            Destroy(gameObject);
        }
    }
}
