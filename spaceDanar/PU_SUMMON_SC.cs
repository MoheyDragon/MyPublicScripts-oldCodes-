using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PU_SUMMON_SC : MonoBehaviour {


    public GameObject SummonedForce;
    public Vector2 SpawnPosition;
    bool IsHit = false;
    void OnTriggerEnter2D(Collider2D other)
        
    {
        if (other.gameObject.tag=="PlayerShip"&&! IsHit)
        {
            IsHit = true;
            AudioManger_Sc.Instance.PlayClip("AlliasPU");
            Destroy(gameObject);
            Instantiate(SummonedForce, new Vector2(SpawnPosition.x, SpawnPosition.y), transform.rotation);
            Instantiate(SummonedForce, new Vector2(-SpawnPosition.x, SpawnPosition.y), transform.rotation);
            Player_Ship_SC.ShipNumbers += 2;
            
        }
       
    }


    




}


