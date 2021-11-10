using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuldingUP_Tutorial : MonoBehaviour
{
    public float Hight, speed;
    public AK.Wwise.Event sound;
    bool soundLock=false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Menu.Pause)
        {
            if (transform.name == "House6")
            {
                if (transform.position.y <= Hight)
                {
                    transform.Translate(new Vector3(0, 0, speed * 0.01f));
                    if (!soundLock)
                    {
                        sound.Post(gameObject);
                        soundLock = true;
                    }
                }
            }
            else if (transform.position.y <= Hight)
            {
                transform.Translate(new Vector3(0, speed * 0.01f, 0));
                if (!soundLock)
                {
                    sound.Post(gameObject);
                    soundLock = true;
                }
            }
            else
            {
                sound.Stop(gameObject);
                Destroy(gameObject.GetComponent<BuldingUP_Tutorial>());
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
