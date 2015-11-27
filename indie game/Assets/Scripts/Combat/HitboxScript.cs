using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class HitboxScript : MonoBehaviour {

    SphereCollider _collider;
    Collider[] colliders;

    public Vector3 velocity;
    private float _duration = 0;
    public int layer = 0;
    public int dmg = 0;
    public int dmgot = 0;
    public int dmgottime = 0;
    public float stunseconds = 0;
    bool spawned = false;
    bool startedDestroy = false;

    private float radius;

    public float slowpercentage = 0;
    public float slowseconds = 0;

    public void Spawn(float delay = 0.0f, float duration = 0.0f)
    {
        _duration = duration;
        StartCoroutine(StartSpawn(delay));
    }

    private IEnumerator StartSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawned = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        radius = gameObject.transform.localScale.x * 0.5f;
        
        if (velocity.magnitude > 0)
        {

            Rigidbody _body = gameObject.AddComponent<Rigidbody>();
            _body.useGravity = false;
            //_body.velocity = velocity;
            _body.AddForce(velocity * 10);
        }
        else
        {
            colliders = Physics.OverlapSphere(transform.position, radius);
            CheckCollisions();  //only do it once
        }

    }

    void Update()
    {
        if(velocity.magnitude > 0 && spawned)
        {
            colliders = Physics.OverlapSphere(transform.position, radius);
            CheckCollisions();
        }
    }

    private void CheckCollisions()
    {
        Collider tempcollider = null;
       

        foreach (Collider col in colliders)
        {
            stats _stats;
           if(col.gameObject.layer == 8)
            {
                _stats = GameObject.FindObjectOfType<PlayerScript>();
            }
            else
            {
                _stats = col.gameObject.GetComponent<stats>();
            }
            
            if (col.gameObject.layer != layer &&  _stats != null)
            {
                if(stunseconds > 0)
                {
                    col.gameObject.GetComponent<EnemyBase>().Stun(stunseconds);
                }
                if(slowpercentage > 0 && slowseconds > 0)
                {
                    _stats.ApplySlow(slowpercentage, slowseconds);
                }
                tempcollider = col;
               _stats.Hit(dmg);
                Debug.Log("dmg: " + dmg);
                if (dmgot != 0)
                {
                    _stats.HitOverTime(dmgot, dmgottime);
                }
                if(velocity.magnitude > 0)
                {
                    break;
                }
            }
        }
        if (!startedDestroy)
        {
            StartCoroutine(DestroySelf());
        }
        else
        {
            if(tempcollider != null)
            {
                DestroySelfNow();
            }
        }
        
    }

    private void DestroySelfNow()
    {
        GameObject.Destroy(this.gameObject);
    }

    private IEnumerator DestroySelf()
    {
        startedDestroy = true;
        yield return new WaitForSeconds(_duration);
        GameObject.Destroy(this.gameObject);
    }

}
