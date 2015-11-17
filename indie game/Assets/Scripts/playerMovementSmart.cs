using UnityEngine;
using System.Collections;

public class playerMovementSmart : MonoBehaviour {

    NavMeshAgent agent;

    [SerializeField]
    Camera camera;
    private Vector3 targetPos;
    private Vector3 directionVector;
    private float movementSpeed = 0.3f;
    [SerializeField]
    Transform movementTransform;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called onc e per frame
	void Update () {
        if (camera == null)
            return;

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            int walkLayerMask = 1 << LayerMask.NameToLayer("Walkable");
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100, walkLayerMask))
            {
                if ((hit.point - movementTransform.position).magnitude > 0.5f)
                {
                    targetPos = hit.point;
                    agent.destination = targetPos;
                }
            }
        }

        

	}
}
