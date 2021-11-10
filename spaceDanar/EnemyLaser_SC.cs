using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser_SC : MonoBehaviour {
    
    public float speed;
    public int PlayerScore, EnemyScore;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -1*transform.up * speed;
        

    }
 
    private void DestroyLaser(GameObject other, int score)
    {
        
        Score_SC.TotalScore += score;
        TextScoreScript.current.UpdateScore();
        Destroy(other);
        Destroy(gameObject);

        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "PlayerLaser":
                DestroyLaser(other.gameObject, PlayerScore);
                break;
            case "EnemyLaser":
                DestroyLaser(other.gameObject, EnemyScore);
                break;
          
        }
    }

}
