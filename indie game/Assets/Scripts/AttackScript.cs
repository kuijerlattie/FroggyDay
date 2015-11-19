using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    GameObject meleeHitbox;
    [SerializeField]
    GameObject HitboxPrefab;
    private float cooldown = 1;
    private float _currentCooldown = 0;
    private float currentCooldown {
        get { return _currentCooldown; }
        set { _currentCooldown = (value > 0)? ((value <= cooldown)? value : cooldown) :  0; } }

    public void MeleeAttack()
    {
        if (currentCooldown == 0)
        {
            currentCooldown = cooldown;
            AttackCollider.MakeAttack(HitboxPrefab, gameObject.transform.position + gameObject.transform.forward, 3, gameObject.transform);
        }
    }

    public void MageAttack()
    {
        if (currentCooldown == 0)
        {
            currentCooldown = cooldown;
            AttackCollider.MakeSpell(HitboxPrefab, gameObject.transform.position + gameObject.transform.forward, 3, gameObject.transform);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //MeleeAttack();
            MageAttack();
            
        }
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

    }
}
