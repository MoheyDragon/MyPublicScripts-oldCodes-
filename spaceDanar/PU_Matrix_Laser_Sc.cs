using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Matrix_Laser_Sc : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag== "PlayerShip")
        {  
            other.gameObject.GetComponent<Player_Ship_SC>().PU_Matrix_LaserEnable();

            AudioManger_Sc.Instance.PlayClip("BulletsDefllectPU_You are the one Neo");
            Destroy(gameObject);
            
        }
    }
    
}
