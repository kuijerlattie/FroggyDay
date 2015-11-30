using UnityEngine;
using System.Collections;

public class EnemyRanged : EnemyBase {

    private float cooldown = 3;
    private float currentcooldown = 0;
    private Vector3 _forward;
    private Vector3 _right;
    private Vector3 _up;

    private float thinktime = 2f;
    private float thinktimer = 2f;

    private Vector3 previousPlayerPos = Vector3.zero;
    private float accuracy = 3;

    private float range = 15;

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunnedSeconds > 0)
        {
            stunnedSeconds -= Time.deltaTime;
            return;
        }
        if (thinktimer >= thinktime)
        {
            Think();
            thinktimer = 0;
        }
        thinktimer += Time.deltaTime;
        previousPlayerPos = target.transform.position;
        if (currentcooldown > 0)
            currentcooldown -= Time.deltaTime;
    }

   private float CurrentDistanceToPlayer()
    {
        return (transform.position - target.transform.position).magnitude;
    }
    private float DestinationDistanceToPlayer()
    {
        return (agent.destination - target.transform.position).magnitude;
    }


    private void Think()
    {
        Vector3 playerVelocity = (target.transform.position - previousPlayerPos).normalized;
        Vector3 forward = (target.transform.position + playerVelocity * accuracy - transform.position).normalized;
        _forward = new Vector3(forward.x, 0, forward.z).normalized;
        _right = new Vector3(-_forward.z, 0, _forward.x).normalized;
        _up = transform.up;
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if(CurrentDistanceToPlayer() > range && DestinationDistanceToPlayer() > range)
        {
            //new destination
        }





        if (distanceToPlayer > range)
        {
            agent.SetDestination(target.transform.position - _forward * range * 0.7f);
            return;
        }

        if (distanceToPlayer <= range * 0.7f)
        {
            if (distanceToPlayer <= range * 0.2f)
                //MeleeAttack(); if it gets too close to the player, it does melee
            agent.SetDestination(target.transform.position - _forward * range);
            return;
        }
      
       if(distanceToPlayer <= range)
       {
            if (currentcooldown <= 0)
            {
                Attack();
                return;
            }
           // agent.SetDestination(GetRandomSide(target.transform.position));
       }
    }

    private Vector3 GetRandomSide(Vector3 origin)   //this needs fixing
    {
        return origin + (new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1))).normalized * (range*0.7f);
    }

    protected void Attack()
    {
        currentcooldown = cooldown;
        GetComponent<AttackScript>().MageAttackForward(0, _forward);
    }
    protected void MeleeAttack()
    {
        currentcooldown = cooldown;
        GetComponent<AttackScript>().MeleeAttack();
    }
}
