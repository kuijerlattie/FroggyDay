using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    float rotation;
    float rotationspeed = 90f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        rotation = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation += rotationspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation -= rotationspeed;
        }

        rotation = rotation * Time.deltaTime;

        Camera.main.transform.RotateAround(transform.position, Vector3.up, rotation);
	}
}
