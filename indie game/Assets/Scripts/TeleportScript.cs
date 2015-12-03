using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour
{

    public GameObject TeleportTarget;

    Collider[] colliders;
    float radius = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //this code replaces a trigger, since triggers refuse to work with the navmeshagent
        colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.layer == 9)
            {
                col.gameObject.transform.position = TeleportTarget.transform.position;
            }
        }
    }
}
