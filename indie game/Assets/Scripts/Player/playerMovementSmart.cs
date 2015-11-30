﻿using UnityEngine;
using System.Collections;

public class playerMovementSmart : MonoBehaviour {

    NavMeshAgent agent;

    [SerializeField] Camera playerCamera;
    GameObject targetPosition;
    float Acceleration = 500;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called onc e per frame
	void Update () {
        if (playerCamera == null)
            return;
        if (targetPosition != null)
        {
            agent.destination = targetPosition.transform.position;
        }
        Debug.Log("pathlength: " + (agent.destination - transform.position).magnitude);
        if((agent.destination - transform.position).magnitude < 1)
        {

        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            LayerMask layermask = (1<<11);
            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit, 100, layermask))
            {
                //if ((hit.point - transform.position).magnitude > 0.5f)
                //{
                    GameObject.Destroy(targetPosition);
                // targetPosition = GameObject.Instantiate(new GameObject());
                    targetPosition = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    targetPosition.transform.position = hit.point;
                    targetPosition.transform.parent = hit.collider.gameObject.transform;
                    
               // }
            }
        }
	}
}
