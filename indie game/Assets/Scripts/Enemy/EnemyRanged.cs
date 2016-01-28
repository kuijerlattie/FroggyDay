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

    System.Random _random = new System.Random();

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.velocity.magnitude > 0)
        {
            GetComponentInChildren<Animator>().SetBool("walk", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("walk", false);
        }
        if (GetComponentInChildren<Animator>().GetBool("attack1"))
        {
            GetComponentInChildren<Animator>().SetBool("attack1", false);
        }
        if (GetComponentInChildren<Animator>().GetBool("attack2"))
        {
            GetComponentInChildren<Animator>().SetBool("attack2", false);
        }

        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        else
        {
            if (target == null)
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
            if (currentmoveCooldown > 0)
            {
                currentmoveCooldown -= Time.deltaTime;
            }
            thinktimer += Time.deltaTime;
            previousPlayerPos = target.transform.position;
            if (currentcooldown > 0)
                currentcooldown -= Time.deltaTime;

            if (agent.destination != targetpos)
            {
                agent.destination = targetpos;
                return;
            }
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

    /// <summary>
    /// this is the Update that is based on time, which is set based on the difficulty of this enemy 
    /// </summary>
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

    /// <summary>
    /// get a position around the target that is never comepletely on the opposite side from the starting point
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="counter"></param>
    /// <returns></returns>
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
        if (_random.Next(0, 2) == 0)
        {
            GetComponentInChildren<Animator>().SetBool("attack1", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("attack2", true);
        }
        currentcooldown = cooldown;
        StartCoroutine(WaitForAttack());
    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.8f);
        GetComponent<AttackScript>().MageAttackForward(1, _forward);
    }

}
