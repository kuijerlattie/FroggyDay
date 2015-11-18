using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    Transform _transform;
    [SerializeField]
    GameObject attackHitbox;

	public void MeleeAttack()
    {
        //GameObject hitbox = (GameObject)GameObject.Instantiate(attackHitbox, _transform.position + _transform.forward, Quaternion.identity);
        //hitbox.transform.parent = _transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MeleeAttack();
        }

    }
}
