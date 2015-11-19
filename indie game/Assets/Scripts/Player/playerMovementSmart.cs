using UnityEngine;
using System.Collections;

public class playerMovementSmart : MonoBehaviour {

    NavMeshAgent agent;

    [SerializeField] Camera playerCamera;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called onc e per frame
	void Update () {
        if (playerCamera == null)
            return;

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            LayerMask layermask = (1<<11);
            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit, 100, layermask))
            {
                if ((hit.point - transform.position).magnitude > 0.5f)
                {
                    agent.destination = hit.point;
                }
            }
        }
	}
}
