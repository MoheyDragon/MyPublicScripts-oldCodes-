using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid_Sc : MonoBehaviour
{
    public float speed, rotationSpeed;
    public int PlayerScore, EnemyScore;
    Rigidbody2D R2;
    public bool RotateRight = true;
    public GameObject exp;
    // Use this for initialization
    void Start()
    {
        R2 = GetComponent<Rigidbody2D>();
        R2.velocity = -1 * transform.up * speed;
        R2.angularVelocity = rotationSpeed;
        int rand = Random.Range(0, 2);
        if (rand == 0)
            RotateRight = false;
    }

        private void DestroyAstroid(GameObject other, int score)
    {
        Score_SC.TotalScore += score;
        TextScoreScript.current.UpdateScore();
      
        Destroy(other);
        Destroy(gameObject);
        Instantiate(exp, transform.position, transform.rotation);
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "PlayerLaser":
                DestroyAstroid(other.gameObject, PlayerScore);
                break;
            case "EnemyLaser":
                DestroyAstroid(other.gameObject, EnemyScore);
                break;
        }
    }

}

   

