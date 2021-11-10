using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SailorAI : MonoBehaviour
{
    NavMeshAgent Agent;
    public GameObject RandomPointInstanite, RandomPoint;
    int index;
    Renderer Rend;
    GameObject[] Cannon_Holders;
    GameObject Door, DestenationParent;
    public Texture[] Textures;
    void Start()
    {
        Door = GameObject.FindGameObjectWithTag("Door");
        DestenationParent = GameObject.FindGameObjectWithTag("DestenationParent");
        Cannon_Holders = GameObject.FindGameObjectsWithTag("Cannon_Holder");
        Rend = GetComponentInChildren<Renderer>();
        Rend.material.mainTexture = Textures[Random.Range(0, Textures.Length)];
        Agent = this.GetComponent<NavMeshAgent>();
        SpwanTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (RandomPoint==null)
        {
            SpwanTarget();
        }
         else  
        {
            Vector3 target = RandomPoint.transform.position - transform.position;
            Agent.SetDestination(RandomPoint.transform.position);
        }
    }
     void SpwanTarget()
    {
        index = Random.Range(0, Manger.instance.Cannon_Holders.Length - 6);
        RandomPoint =Cannon_Holders[index];
        Instantiate(RandomPointInstanite, RandomPoint.transform.position, Quaternion.identity, DestenationParent.transform);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "RandomPoint")
        {
            RandomPoint = null;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "EdgeCollider" && !SantaMaria.RamLock)
        {
            Instantiate(this, Door.transform.position, Quaternion.identity, transform.parent);
            RandomPoint = null;
            Destroy(gameObject);

        }

    }
   

}
