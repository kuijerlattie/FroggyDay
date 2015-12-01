using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class minimapPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = new Vector2(Screen.width - 250, Screen.height - 250); //minimap position 
        GetComponent<Image>().rectTransform.position = pos;
        GetComponent<Image>().rectTransform.pivot = Vector2.zero;
    }
}
