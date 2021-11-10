using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuldingUpDown : MonoBehaviour
{
    public float CoolDown, lowHight, speed;
    public float MainCoolDown, origin;
    bool Lock = false;
    Rigidbody rb;
    public AK.Wwise.Event sound;
    bool soundLock;
    // Start is called before the first frame update
    void Start()
    {
        soundLock = false;
        origin = this.transform.position.y;
        rb = this.GetComponent<Rigidbody>();
        MainCoolDown = CoolDown;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= CoolDown&&!Menu.Pause)
        {
            if (transform.position.y >= lowHight && !Lock)
            {
                transform.position += new Vector3(0, -speed * 0.01f, 0);
                if (!soundLock)
                {
                    sound.Post(gameObject);
                    soundLock = true;
                }
            }
            else if (transform.position.y <= lowHight && !Lock)
            {
                sound.Stop(gameObject);
                CoolDown = Time.time + MainCoolDown;
                Lock = true;
            }
            else if (Lock && transform.position.y <= origin)
            {
                transform.position += new Vector3(0, speed * 0.01f, 0);
                if (soundLock)
                {
                    sound.Post(gameObject);
                    soundLock = false;
                }
            }
            else if (transform.position.y >= origin)
            {
                CoolDown = Time.time + MainCoolDown;
                sound.Stop(gameObject);
                Lock = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") other.GetComponent<CharecterController>().BulidingParent = this.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") other.GetComponent<CharecterController>().BulidingParent = null;
    }
}
