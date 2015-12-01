using UnityEngine;
using System.Collections;

public class EnemyRanged : EnemyBase {

    private float cooldown = 3;
    private float currentcooldown = 0;
    private Vector3 _forward;
    private Vector3 _right;
    private Vector3 _up;

    private float thinktime = 0.5f;
    private float thinktimer = 0.5f;

    private Vector3 previousPlayerPos = Vector3.zero;
    private float accuracy = 3;

    private float minRange = 10;
    private float maxRange = 15;

    private float moveCooldown = 5;
    private float currentmoveCooldown = 0;

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if(target == null)
        {
            return; //player is dead, AI shouldnt do anything
        }
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
        if(currentmoveCooldown > 0)
        {
            currentmoveCooldown -= Time.deltaTime;
        }
        thinktimer += Time.deltaTime;
        previousPlayerPos = target.transform.position;
        if (currentcooldown > 0)
            currentcooldown -= Time.deltaTime;

        if(agent.destination != targetpos)
        {
            agent.destination = targetpos;
            return;
        }
    }

   private float CurrentDistanceToPlayer()
    {
        return (transform.position - target.transform.position).magnitude;
    }
    private float DestinationDistanceToPlayer()
    {
        return (agent.destination - target.transform.position).magnitude;
    }

    private bool InDesiredRange()
    {
        return CurrentDistanceToPlayer() <= maxRange && CurrentDistanceToPlayer() >= minRange;
    }

    private bool CheckDestination()
    {
        return DestinationDistanceToPlayer() <= maxRange && DestinationDistanceToPlayer() >= minRange;
    }

    private void Think()
    {
        Vector3 playerVelocity = (target.transform.position - previousPlayerPos).normalized;
        Vector3 forward = (target.transform.position + playerVelocity * accuracy - transform.position).normalized;
        _forward = new Vector3(forward.x, 0, forward.z).normalized;
        _right = new Vector3(-_forward.z, 0, _forward.x).normalized;
        _up = transform.up;

        if(InDesiredRange() && agent.velocity.magnitude == 0 && currentcooldown <= 0)
        {
            Attack();
            return;
        }

        if(!InDesiredRange() && !CheckDestination())
        {
            //new destination
            if (currentmoveCooldown <= 0)
            {
                currentmoveCooldown = moveCooldown;
                targetpos = GetRandomSide(target.transform.position);
                return;
            }
            else
            {
                if(currentcooldown <= 0)
                {
                    Attack();
                }
            }
        }
    }

    private Vector3 GetRandomSide(Vector3 origin, int counter = 0)
    {
        Vector3 newpos = origin + (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f))).normalized * maxRange * 0.9f;
        if(counter >= 5)
        {
            return newpos;
        }
        if((newpos - transform.position).magnitude > (target.transform.position - transform.position).magnitude * 1.5f)
        {
            return GetRandomSide(origin, counter++);
        }
        return newpos;
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
