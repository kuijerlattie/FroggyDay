using UnityEngine;
using System.Collections;

public class MouseHover : MonoBehaviour {

    public GameObject selectedObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseEnter()
    {
        Debug.Log(true);
        selectedObject.SetActive(true);
    }
    void OnMouseOver()
    {
        selectedObject.SetActive(false);
    }



}
