using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SailorAI : MonoBehaviour
{
    NavMeshAgent Agent;
    int index;
    public GameObject[] Targets;
    Animator anime;
    void Start()
    {
        Agent = gameObject.GetComponent<NavMeshAgent>();
        anime = gameObject.GetComponent<Animator>();
        anime.SetBool("Walk", true);
        index = Random.Range(0, Targets.Length);
        Agent.SetDestination(Targets[index].transform.position);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            Agent.isStopped = true;
            anime.SetBool("Walk", false);
            StartCoroutine(newDestination());
        }
    }
    IEnumerator newDestination()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));
        index = Random.Range(0, Targets.Length);
        Agent.SetDestination(Targets[index].transform.position);
        anime.SetBool("Walk", true);
        Agent.isStopped = false;
    }
}
