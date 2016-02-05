using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public Item item;
    public GameObject gameobject;
    float colliderscale = 2;
	// Use this for initialization
	void Start () {
        GetComponent<Collider>().isTrigger = true;
        Rigidbody rigid = gameObject.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
        transform.localScale = new Vector3(50, 50, 50);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == 8)
        {
            Debug.Log("Picked up");
            GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.PickUpItem);
            item.Pickup();
            GameObject.Destroy(gameobject);
        }
    }

}
