using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    public float rotation;
    float rotationspeed = 90f;
    float maxZoomDistance = 50f; //max distance away from player
    float minZoomDistance = 10f; //min distance away from player
    float oldZoomDistance = 0;
    float targetZoomDistance = 50f;
    float zoomDistance = 50f;

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
            rotation += rotationspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation -= rotationspeed;
        }
        if (oldZoomDistance != 0)
        {
            targetZoomDistance = oldZoomDistance;
            oldZoomDistance = 0;
        }

        targetZoomDistance -= Input.GetAxis("Mouse ScrollWheel");
        if (targetZoomDistance < minZoomDistance)
            targetZoomDistance = minZoomDistance;
        if (targetZoomDistance > maxZoomDistance)
            targetZoomDistance = maxZoomDistance;

        LayerMask layermask = (1 << 10); //layer 10 = Hideablewall
        if (Physics.Raycast(player.transform.position, -(player.transform.position - Camera.main.transform.position), out hit, zoomDistance + 1, layermask))
        {
            if (zoomDistance > hit.distance)
            {
                if (zoomDistance > targetZoomDistance)
                {
                    oldZoomDistance = zoomDistance;
                }
                else
                {
                    oldZoomDistance = targetZoomDistance;
                }
                zoomDistance = hit.distance;
            }
        }
        else
        {
            if (targetZoomDistance > zoomDistance)
                zoomDistance += 1;
            else
                zoomDistance = targetZoomDistance;
        }
        rotation = rotation * Time.deltaTime;

        Camera.main.transform.position = player.transform.position;
        Camera.main.transform.RotateAround(player.transform.position, Vector3.up, rotation);
        Camera.main.transform.position = player.transform.position - Camera.main.transform.forward * zoomDistance;
        //Vector3 newcameraposition;//

        //newcameraposition = (player.transform.position - Camera.main.transform.position).normalized;
        //newcameraposition *= zoomDistance;
        

        //Camera.main.transform.position = newcameraposition; //makes camera zoom in somewhere... not at the place where i want it tho
               

        
	}

 //copy pasta.fbx.txt.meta

}
