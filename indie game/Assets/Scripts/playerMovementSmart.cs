using UnityEngine;
using System.Collections;

public class playerMovementSmart : MonoBehaviour {

    NavMeshAgent agent;

    [SerializeField] Camera playerCamera;
    [SerializeField] Transform playerTransform;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called onc e per frame
	void Update () {
        if (playerCamera == null)
            return;

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if ((hit.point - playerTransform.position).magnitude > 0.5f)
                {
                    agent.destination = hit.point;
                }
            }
        }

        

	}
}
