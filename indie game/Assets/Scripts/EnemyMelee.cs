using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBase {

	// Use this for initialization
	void Start () {
        base.Start();
        attackrange = 2;
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, target.transform.position) <= attackrange)
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

    }
}
