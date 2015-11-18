using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class HitboxScript : MonoBehaviour {

    SphereCollider _collider;

    void Start()
    {
        
    }

	public void Activated()
    {
        Debug.Log("attacked");
        _collider = GetComponent<SphereCollider>();
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, _collider.radius);
       //ebug.Log(colliders.Length);
        
        foreach(Collider col in colliders)
        {
            if(col.gameObject.GetComponentInChildren<EnemyBase>() != null)
            {
                Debug.Log(true);
            }
        }
        //is.gameObject.SetActive(false);
    }
}
