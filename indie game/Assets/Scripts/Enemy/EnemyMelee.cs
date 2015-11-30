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
        if(stunnedSeconds > 0)
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

    protected void Attack()
    {
        currentcooldown = cooldown;
        GetComponent<AttackScript>().MeleeAttack();
    }
}
