using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Animator anime;
    public LevelManager level;
    int directionAim;
    Vector3 rotaiton180 = new Vector3(0,180,0);
    Vector3 rotaiton0 = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        directionAim = 0;
        anime = GetComponent<Animator>();
    }

    public void attack(int direction)
    {
        if (direction==2)
            transform.eulerAngles = rotaiton180;
        else
            transform.eulerAngles = rotaiton0;
        anime.SetTrigger("Attack");
        directionAim= direction;
    }
    public void fire()
    {
        level.fire(directionAim);
    }
}
