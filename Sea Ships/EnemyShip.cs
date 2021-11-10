using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShip : ShipMain
{
    
    protected NavMeshAgent agent; public float RecoilTime, EngageDistance, SpotingDistance;
    List<Renderer> rend = new List<Renderer>();
    List<Texture> MainTexture = new List<Texture>();
    protected Transform Player; protected float Distance, attackTime;
    bool WaveLock; bool Waver = false;
    float PreviousRotation;
    protected bool Freezelook = false;

    public override void Start()
    {
        base.Start();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = MoveSpeed;

    }
    public override void Awake()
    {
        base.Awake();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            rend.Add(renderer);
            MainTexture.Add(renderer.material.mainTexture);
        }
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Distance = Vector3.Distance(Player.position, transform.position);
        if (Distance>SpotingDistance)
        {
            Anime.SetBool("Moving", false);
        }
        if (Distance <= SpotingDistance && agent.isStopped == false)
        {
            agent.SetDestination(Player.position);
            Anime.SetBool("Moving", true);
        }
        if (Distance <= EngageDistance && Freezelook == false)
        {
            engage();
        }
        if (Distance >= EngageDistance && Freezelook == false)
        {
            agent.isStopped = false;
        }
        if (PreviousRotation != transform.rotation.y&& WavesHolder.childCount > 0)
        {
            WavesParticle.loop = false;
            WavesHolder.DetachChildren();
            WaveLock = false;
        }
        if (Mathf.Abs(PreviousRotation - transform.rotation.y)<1 && !WaveLock)
        {
            if (!Waver)
            {
                Invoke("WaveInstantiate", 0.5f);
            }
            WaveLock = true;

        }


        PreviousRotation =transform.rotation.y;
    }

    public void Freeze(int damage, int Time)
    {
        Freezelook = true;
        if (Anime.GetBool("Moving"))
        {
            Anime.SetBool("Moving", false);
        }
        Health -= damage;
        Health = Mathf.Max(0, Health);
        if (Health <= 0)
            Destroy(gameObject);
        else 
            StartCoroutine(FreezeTime(Time));

        
    }
    IEnumerator  FreezeTime(int Time)
    {
        transform.Translate(Vector3.zero);
        agent.isStopped = true;
        foreach (Renderer renderer in rend)
        {
                renderer.material.mainTexture = null;
        }
        
        yield return new WaitForSeconds(Time);
        foreach (Renderer renderer in rend)
        {
                int index = rend.IndexOf(renderer);
                renderer.material.mainTexture = MainTexture[index];
        }
        agent.isStopped = false;
        agent.speed = MoveSpeed;
        Freezelook = false;
        Anime.SetBool("Moving", true);
    }

    public virtual void  engage()
    {
        
        agent.isStopped = true;
        Anime.SetBool("Moving", false);
    }
    public override void WaveInstantiate()
    {
        base.WaveInstantiate();
        Waver = false;
    }
}
