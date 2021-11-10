using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    readonly string Enemy = "enemy";
    readonly string finish = "spawnPoint";
    float speed = 0.2f;
    public int damage = 1;
    WaitForSeconds delay =new WaitForSeconds(0.25f);
    bool EnterLock;
    private void Start()
    {
        EnterLock = false;
    }
    void Update()
    {
        transform.Translate(Vector3.forward*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(finish))
            gameObject.SetActive(false);
        else if (other.CompareTag(Enemy))
        {
            if (EnterLock)
            {
                EnterLock = false;
                gameObject.SetActive(false);
            }
            else
            {
                other.GetComponent<Enemy>().damage(damage);
                StartCoroutine(fade());
            }
        }
        
    }
    IEnumerator fade()
    {
        EnterLock = true;
        yield return delay;
        gameObject.SetActive(false);
        EnterLock = false;
    }
}
