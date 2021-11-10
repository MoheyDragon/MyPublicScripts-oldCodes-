using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Matrix_Astroid_SC : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            other.gameObject.GetComponent<Player_Ship_SC>().PU_Matrix_AstroidEnable();

            AudioManger_Sc.Instance.PlayClip("PU_Astroid_YouAreSpaceNightmare");
            Destroy(gameObject);

        }
    }
}
