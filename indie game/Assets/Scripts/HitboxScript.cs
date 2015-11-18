using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class HitboxScript : MonoBehaviour {

    SphereCollider _collider;

	public void Activated()
    {
        _collider = GetComponent<SphereCollider>();
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, _collider.radius);
        
        foreach(Collider col in colliders)
        {
          if(col.gameObject.layer != gameObject.layer)
            {
                col.gameObject.GetComponent<stats>().Hit(1);
            }
        }
        gameObject.SetActive(false);
    }
}
