using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class HitboxScript : MonoBehaviour {

    SphereCollider _collider;
    Collider[] colliders;
    private float _duration = 0;
    public int layer = 0;

    public void Spawn(float delay = 0.0f, float duration = 0.0f)
    {
        _duration = duration;
        StartCoroutine(StartSpawn(delay));
    }

    private IEnumerator StartSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _collider = GetComponent<SphereCollider>();
        colliders = Physics.OverlapSphere(gameObject.transform.position, _collider.radius);
        CheckCollisions();

    }
    private void CheckCollisions()
    {
        foreach (Collider col in colliders)
        {
            if (col.gameObject.layer != layer)
            {
                if (col.gameObject.GetComponent<stats>() != null) 
                col.gameObject.GetComponent<stats>().Hit(1);
            }
        }
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        gameObject.transform.parent = null;
        yield return new WaitForSeconds(_duration);
        GameObject.Destroy(this.gameObject);
    }

}
