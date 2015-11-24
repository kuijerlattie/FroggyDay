using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    float rotation;
    float rotationspeed = 90f;
    float maxZoomDistance = 50f; //max distance away from player
    float minZoomDistance = 10f; //min distance away from player
    float zoomDistance = 100f;

    RaycastHit hit;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation -= rotationspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation += rotationspeed;
        }
        zoomDistance -= Input.GetAxis("Mouse ScrollWheel");
        if (zoomDistance < minZoomDistance)
            zoomDistance = minZoomDistance;
        if (zoomDistance > maxZoomDistance)
            zoomDistance = maxZoomDistance;

        rotation = rotation * Time.deltaTime;

        Camera.main.transform.position = player.transform.position;
        Camera.main.transform.RotateAround(player.transform.position, Vector3.up, rotation);
        Camera.main.transform.position = player.transform.position - Camera.main.transform.forward * zoomDistance;
        //Vector3 newcameraposition;//

        //newcameraposition = (player.transform.position - Camera.main.transform.position).normalized;
        //newcameraposition *= zoomDistance;
        

        //Camera.main.transform.position = newcameraposition; //makes camera zoom in somewhere... not at the place where i want it tho
               

        LayerMask layermask = (1 << 10);
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(player.transform.position), out hit, 100, layermask))
        if (Physics.Raycast(player.transform.position, (Camera.main.transform.position - player.transform.position), out hit, 100, layermask))
        {
            Debug.Log("wall spotted");
            //get distance from player to wall
            //set camera at that distance
        }
	}

 //copy pasta.fbx.txt.meta

}
