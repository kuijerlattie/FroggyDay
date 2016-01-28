using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBase {

    private float cooldown = 2;
    private float currentcooldown = 0;
  


	// Use this for initialization
	void Start () {
        base.Start();
	}

   
	
	// Update is called once per frame
	void Update () {

        if(agent.velocity.magnitude > 0)
        {
            GetComponentInChildren<Animator>().SetFloat("walk", 1);
        }
        else
        {
            GetComponentInChildren<Animator>().SetFloat("walk", 0);
        }
        if (GetComponentInChildren<Animator>().GetBool("attack"))
        {
        GetComponentInChildren<Animator>().SetBool("attack", false);
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
        GetComponentInChildren<Animator>().SetBool("attack", true);
        currentcooldown = cooldown;
        GetComponent<AttackScript>().MeleeAttack();

    }
}
