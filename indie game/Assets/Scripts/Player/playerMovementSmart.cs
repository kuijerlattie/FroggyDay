﻿using UnityEngine;
using System.Collections;

public class playerMovementSmart : MonoBehaviour {

    NavMeshAgent agent;

    [SerializeField] Camera playerCamera;
    public GameObject targetPosition;
    float Acceleration = 500;
    FMODUnity.StudioEventEmitter emitter;
    bool walking = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        targetPosition = GameObject.CreatePrimitive(PrimitiveType.Cube);
        targetPosition.transform.position = transform.position;
    }
	
	// Update is called onc e per frame
	void Update () {
        if (agent.velocity.magnitude > 0)
        {
            if(walking == false)
                emitter = GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.Footstep);
            walking = true;
            GetComponentInChildren<Animator>().SetFloat("speed" , 0.2f);
        }
        else
        {
            walking = false;
            GetComponentInChildren<Animator>().SetFloat("speed", 0.0f);
            GameObject.FindObjectOfType<SoundManager>().StopLooping(emitter);
        }
        if (playerCamera == null)
            return;
        if (targetPosition != null)
        {
            agent.destination = targetPosition.transform.position;
        }
        if((agent.destination - transform.position).magnitude < 0.4f)
        {
            //attempt to fix weird behaviour on rotating navmesh
            // agent.Warp(agent.destination);
            //transform.position = agent.destination;
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            LayerMask layermask = (1<<11);
            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit, 100, layermask))
            {
                 targetPosition.transform.position = hit.point;
                 targetPosition.transform.parent = hit.collider.gameObject.transform;
            }
        }
	}
}
