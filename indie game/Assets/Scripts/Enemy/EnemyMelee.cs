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

        if (target.GetComponent<PlayerScript>().health <= 0)
        {
            stunnedSeconds = 2;
        }
        if (agent.velocity.magnitude > 0)
        {
            GetComponentInChildren<Animator>().SetFloat("speed", 1);
        }
        else
        {
            GetComponentInChildren<Animator>().SetFloat("speed", 0);
        }
        if (GetComponentInChildren<Animator>().GetBool("attack1"))
        {
        GetComponentInChildren<Animator>().SetBool("attack1", false);
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
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.ESmallAttack);
        GetComponentInChildren<Animator>().SetBool("attack1", true);
        currentcooldown = cooldown;
        GetComponent<AttackScript>().MeleeAttack();

    }

    public override void Die()
    {
        if (manager == null) manager = GameObject.FindObjectOfType<EnemyManager>();
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.ESmallDeath);
        manager.RemoveEnemy();
        if (canDrop)
        {
            DropRandomPickup(transform.position);
            DropAlwaysDrop();
        }


        GameObject.FindObjectOfType<PlayerScript>().LootGold(gold);
        GameObject.Destroy(gameObject);
    }
}
