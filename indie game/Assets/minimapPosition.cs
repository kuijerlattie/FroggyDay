using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class minimapPosition : MonoBehaviour {

    public float xoffset = 0;
    public float yoffset = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = new Vector2(Screen.width - 250 + xoffset, Screen.height - 250 + yoffset); //minimap position 
        GetComponent<Image>().rectTransform.position = pos;
        GetComponent<Image>().rectTransform.pivot = Vector2.zero;
    }
}
