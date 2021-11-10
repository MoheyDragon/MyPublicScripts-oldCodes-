using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip_SC : MonoBehaviour
{
    public float speed;
    public int PlayerScore, EnemyScore, HitScore, FrinedHitScore;
    public GameObject EnemyLaser, exp;
    public Transform ShootingPoint;
    Rigidbody2D R2;
    public int CurrentScore
    {
        get { return Score_SC.TotalScore; }
        set
        {
            Score_SC.TotalScore = value;
            TextScoreScript.current.UpdateScore();
        }
    }

    // Use this for initialization
    void Start()
    {
        R2 = GetComponent<Rigidbody2D>();
        R2.velocity = -1 * transform.up * speed;
        InvokeRepeating("FireLaser", 1f, 2f);
    }
    private void DestroyShip(GameObject other,int score)
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
                    DestroyShip(other.gameObject, PlayerScore);
                    break;
                case "EnemyLaser":
                    DestroyShip(other.gameObject, EnemyScore);
                    break;
                case "PlayerShip":
                    Score_SC.TotalScore += HitScore;
                    break;
                case "EnemyShip":
                    DestroyShip(other.gameObject, FrinedHitScore);
                    break;
            }
       
       
    }
    void FireLaser()
    {
        Instantiate(EnemyLaser, ShootingPoint.position, transform.rotation);
        AudioManger_Sc.Instance.PlayClip("Enemy");
    }


}
