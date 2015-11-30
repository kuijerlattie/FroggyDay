using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {

    public GameObject TeleportTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "player")
        {
            collision.collider.gameObject.transform.position = TeleportTarget.transform.position;
        }
    }
}
