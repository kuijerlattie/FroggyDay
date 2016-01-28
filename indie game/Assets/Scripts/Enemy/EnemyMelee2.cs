using UnityEngine;
using System.Collections;

public class EnemyMelee2 : EnemyBase
{

    private float cooldown = 2;
    private float currentcooldown = 0;
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
            if (stunnedSeconds > 0)
            {
                agent.SetDestination(transform.position);
                stunnedSeconds -= Time.deltaTime;
                return;
            }

            if (Vector3.Distance(transform.position, target.transform.position) <= GetComponent<NavMeshAgent>().stoppingDistance)
            {
                if (currentcooldown <= 0)
                {
                    Attack();
                }
            }
            else
            {

                agent.SetDestination(target.transform.position);
            }

            if (currentcooldown > 0)
                currentcooldown -= Time.deltaTime;
        }
    }

    protected void Attack()
    {
        if(_random.Next(0,2) == 0)
        {
            GetComponentInChildren<Animator>().SetBool("attack1", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("attack2", true);
        }
        
        
        currentcooldown = cooldown;
        GetComponent<AttackScript>().MeleeAttack();

    }
}
