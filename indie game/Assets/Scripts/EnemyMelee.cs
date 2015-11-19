using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBase {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, target.transform.position) <= GetComponent<NavMeshAgent>().stoppingDistance)
        {
            Attack();
        }
        else
        {
            agent.SetDestination(target.transform.position);    
        }
	}

    protected void Attack()
    {
        GetComponent<AttackScript>().MeleeAttack();
    }
}
