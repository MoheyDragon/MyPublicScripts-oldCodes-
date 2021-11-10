using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner_old_Sc : MonoBehaviour {
    public GameObject asteroid;
    public Vector2 pos;
    void   Start ()
    {
        InvokeRepeating("AstroidSpawner", 1f, Random.Range(1f, 5f));
    }
    void AstroidSpawner()
    {
        Vector2 newPos = new Vector2(Random.Range(-pos.x, pos.x), pos.y);
        Instantiate(asteroid, newPos, transform.rotation);
    }
}
