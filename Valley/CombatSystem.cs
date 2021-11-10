using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    Animator anime;
    public int Health = 100;
    CharecterController player;
    // Start is called before the first frame update
    void Start()
    {
        anime = gameObject.GetComponent<Animator>();
        player= gameObject.GetComponent<CharecterController>();
        player.CombatLock = false;
        player.IsCombating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsCombating)
        {
            if (Input.GetMouseButton(0))
                anime.SetTrigger("Attack1");
            if (Input.GetMouseButton(1))
            {
                anime.SetTrigger("Attack2");
                player.CombatLock = true;
            }
        }
        
    }
    public void Damage(int dam)
    {
        Health -= dam;
        player.HitHero.Post(gameObject);
    }
    public void Attack(int damage)
    {
        if (Physics.CheckSphere(transform.position,5))
        {
           Collider[] colliders=  Physics.OverlapSphere(transform.position, 5);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                   col.gameObject.GetComponent<EnemyAi>().Damage(damage);
                    player.HitEnemy.Post(gameObject);
                }
                if (col.CompareTag("Tree"))
                {
                    col.gameObject.GetComponent<Tree>().Hit(damage, player.treeDrop);
                    player.HitTree.Post(gameObject);

                }

            }
        }
        player.CombatLock = false;
        player.IsCombating = false;
    }
    public void TriggerOff(int number)
    {
        if (number==1)
            anime.ResetTrigger("Attack1");
        else
            anime.ResetTrigger("Attack2");
        player.IsCombating = true;
    }
}
