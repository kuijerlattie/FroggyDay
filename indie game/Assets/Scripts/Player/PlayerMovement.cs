using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] Camera camera;
	private Vector3 targetPos;
	private Vector3 directionVector;
	private float movementSpeed = 0.3f;
	[SerializeField] Transform movementTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (camera == null)
			return;

		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			int walkLayerMask = 1 << LayerMask.NameToLayer("Walkable");
			if (Physics.Raycast (camera.ScreenPointToRay (Input.mousePosition), out hit, 100, walkLayerMask)) {
				if((hit.point - movementTransform.position).magnitude > 0.5f){
					targetPos = hit.point;
				}
			}
		}

		directionVector = targetPos - movementTransform.position;

		if (directionVector.magnitude > movementSpeed) {
			movementTransform.position = movementTransform.position + directionVector.normalized * movementSpeed;

			Vector3 newForward = new Vector3(directionVector.normalized.x, 0, directionVector.normalized.z);
			transform.forward = newForward;
		} else {
			movementTransform.position = targetPos;
		}


		/*
		Collider[] colliders = Physics.OverlapSphere (feetCollider.transform.position, feetCollider.radius);
		foreach (Collider collider in colliders) {
			if(collider.gameObject.layer == LayerMask.NameToLayer("Walkable")){
				return;
			}
		}
		movementTransform.position = new Vector3 (movementTransform.position.x, movementTransform.position.y - 0.1f, movementTransform.position.z);
		*/



	}
}
