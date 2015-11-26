using UnityEngine;
using System.Collections;

public class LavaScript : MonoBehaviour {

    public float vScrollSpeed;
    public float hScrollSpeed;
    Vector2 offset = new Vector2(0,0);

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        offset.x += (vScrollSpeed * Time.deltaTime);
        offset.y += (hScrollSpeed * Time.deltaTime);
        
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);

	}
}
