using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Boundry
{
    public float MinX, MaxX, MinY, MaxY;
}
public class Player_Ship_SC : MonoBehaviour
{

    float MH, MV;
    public float Speed = 5f;
    public float Reload, ReloadTime;
    public static bool PU_Matrix_Laser, PU_Matrix_Astroid, PU_Summon;
    public static int ShipNumbers = 1;
    private bool Pause = true;
    public AudioSource Source;



    public Transform PlayerShooter;
    public Boundry boundry;
    public GameObject playerLaser, exp;

    public Image BackLaser, BackAstroid;

    public static List<Player_Ship_SC> PlayerShips = new List<Player_Ship_SC>();

    Vector2 movement;
    Rigidbody2D R2;

    void Start()
    {
        PlayerShips.Add(this);

        PU_Summon = PU_Matrix_Astroid = PU_Matrix_Laser = false;
        R2 = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        if (Input.GetButton("Jump") && Time.time - Reload > ReloadTime || Input.GetButton("Fire1") && Time.time - Reload > ReloadTime)
        {
            Instantiate(playerLaser, PlayerShooter.position, transform.rotation);
            Reload = Time.time;
            AudioManger_Sc.Instance.PlayClip("Player");
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Pause == true)
            {

                Source.Pause();
                Time.timeScale = 0;
                Pause = false;
            }
            else if (Pause == false)
            {
                Source.Play();
                Time.timeScale = 1;
                Pause = true;
            }

        }
    }

    Vector2 StartPos;

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartPos = Input.mousePosition;
                Debug.Log(StartPos);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log((Vector2)Input.mousePosition - StartPos);
                Player_Ship_SC.MoveShips((Vector2)Input.mousePosition - StartPos);
            }
        }

        //MH = Input.GetAxis("Horizontal");
        //MV = Input.GetAxis("Vertical");
        //movement = new Vector2(MH, MV);
        //R2.velocity = movement * Speed;
        //transform.position = new Vector2(Mathf.Clamp
        //    (transform.position.x, boundry.MinX, boundry.MaxX), Mathf.Clamp(transform.position.y, boundry.MinY, boundry.MaxY));
    }

    public static void MoveShips(Vector2 moveVector)
    {
        foreach (var playerScript in PlayerShips)
            playerScript.Move(moveVector);
    }
    public void Move(Vector2 moveVector)
    {
        R2.velocity = moveVector * Speed;
        transform.position = new Vector2(Mathf.Clamp
            (transform.position.x, boundry.MinX, boundry.MaxX), Mathf.Clamp(transform.position.y, boundry.MinY, boundry.MaxY));

    }
    private void DestroyShip(GameObject other)
    {

        ShipNumbers -= 1;
        Destroy(gameObject);
        Destroy(other);
        Instantiate(exp, transform.position, transform.rotation);
        if (ShipNumbers == 0)
        {
            AudioManger_Sc.Instance.PlayClip("explosion");
            Source.Stop();
            UiManager_Sc.Instance.Score.enabled = false;
            UiManager_Sc.Instance.BackLaser.enabled = false;
            UiManager_Sc.Instance.BackAstroid.enabled = false;
            ShipNumbers = 1;
            UiManager_Sc.Instance.FinalScore.text = Score_SC.TotalScore.ToString();
            UiManager_Sc.Instance.PanelGameOver.SetActive(true);
            var _Ships = GameObject.FindGameObjectsWithTag("PlayerShip");
            for (int i = _Ships.Length - 1; i >= 0; i--)
            {
                Destroy(_Ships[i]);
            }
        }


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "Enemy":
                if (PU_Matrix_Astroid == false)
                {

                    DestroyShip(other.gameObject);
                }
                break;
            case "EnemyLaser":
                if (PU_Matrix_Laser == false)
                {
                    DestroyShip(other.gameObject);
                }

                break;

        }
    }
    public void PU_Matrix_LaserEnable()
    {
        PU_Matrix_Laser = true;
        UiManager_Sc.Instance.BackLaser.enabled = true;
        Invoke("PU_Matrix_LaserDisable", 30f);
    }
    void PU_Matrix_LaserDisable()
    {
        PU_Matrix_Laser = false;
        UiManager_Sc.Instance.BackLaser.enabled = false;
    }

    public void PU_Matrix_AstroidEnable()
    {
        PU_Matrix_Astroid = true;
        UiManager_Sc.Instance.BackAstroid.enabled = true;
        Invoke("PU_Matrix_AstroidDisable", 30f);


    }
    void PU_Matrix_AstroidDisable()
    {
        PU_Matrix_Astroid = false;
        UiManager_Sc.Instance.BackAstroid.enabled = false;
    }
    public void MoveLeft()
    {
        var newPos = new Vector2(transform.position.x - 10, transform.position.y);

        transform.position = Vector2.Lerp(transform.position, newPos, 0.5f * Time.deltaTime);
        //R2.velocity = movement * Speed;
        transform.position = new Vector2(Mathf.Clamp
            (transform.position.x, boundry.MinX, boundry.MaxX), Mathf.Clamp(transform.position.y, boundry.MinY, boundry.MaxY));
    }
    public void MoveRight()
    {
        R2.velocity = new Vector2(0, 1);
    }
    public void StopShip()
    {
        R2.velocity = Vector2.zero;
    }

}
