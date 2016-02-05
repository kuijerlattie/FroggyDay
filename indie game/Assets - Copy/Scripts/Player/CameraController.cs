using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    public float rotation;
    float rotationspeed = 90f;
    float maxZoomDistance = 30f; //max distance away from player
    float minZoomDistance = 10f; //min distance away from player
    float oldZoomDistance = 0;
    float targetZoomDistance = 50f;
    float zoomDistance = 50f;
    public bool lockRotation = false;

    RaycastHit hit;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

    private void UpdateTextRotation()
    {
        //points all textboxes ingame towards the camera
        PointToCamera[] canvasses = FindObjectsOfType<PointToCamera>();
        foreach (PointToCamera canvas in canvasses)
        {
            canvas.UpdateRotation(transform.rotation);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!lockRotation)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotation += rotationspeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotation -= rotationspeed;
            }
            if (oldZoomDistance != 0)
            {
                targetZoomDistance = oldZoomDistance;
                oldZoomDistance = 0;
            }
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
        UpdateTextRotation();
       
	}
}
