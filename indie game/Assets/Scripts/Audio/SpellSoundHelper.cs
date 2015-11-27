using UnityEngine;
using System.Collections;

public class SpellSoundHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<AudioSource>().isPlaying)
            GameObject.Destroy(gameObject);
	}
}
