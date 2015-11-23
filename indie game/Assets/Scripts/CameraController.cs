using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    float rotation;
    float rotationspeed = 90f;
    float maxZoomDistance = 100f; //max distance away from player
    float minZoomDistance = 10f; //min distance away from player
    float zoomDistance = 100f;

    RaycastHit hit;
    GameObject player;
    GameObject disabledWall;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
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
        zoomDistance += Input.GetAxis("Mouse ScrollWheel");
        if (zoomDistance < minZoomDistance)
            zoomDistance = minZoomDistance;
        if (zoomDistance > maxZoomDistance)
            zoomDistance = maxZoomDistance;


        rotation = rotation * Time.deltaTime;

        Camera.main.transform.RotateAround(transform.position, Vector3.up, rotation);

        if (disabledWall != null)
        {
            disabledWall.SetActive(true);
            disabledWall = null;
        }
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
