using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    GameObject meleeHitbox;

	public void MeleeAttack()
    {
        meleeHitbox.SetActive(true);
        meleeHitbox.GetComponent<HitboxScript>().Activated();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MeleeAttack();
        }

    }
}
