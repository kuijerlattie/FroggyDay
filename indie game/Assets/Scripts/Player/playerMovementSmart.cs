using UnityEngine;
using System.Collections;

public class playerMovementSmart : MonoBehaviour {

    NavMeshAgent agent;

    [SerializeField] Camera playerCamera;
    public GameObject targetPosition;
    float Acceleration = 500;
    FMODUnity.StudioEventEmitter emitter;
    FmodPlayScript footsteps = null;
    bool walking = false;
    PlayerScript player;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        targetPosition = GameObject.CreatePrimitive(PrimitiveType.Cube);
        targetPosition.transform.position = transform.position;
        player = GetComponent<PlayerScript>();
    }
	
	// Update is called onc e per frame
	void Update () {
        if (agent.velocity.magnitude > 0)
        {

            if (footsteps == null || !footsteps.isPlaying)
            {
                footsteps = GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.Footstep);
                Debug.Log("triggeredonce?: " + footsteps.emitter.TriggerOnce);
            }
            
            walking = true;
            GetComponentInChildren<Animator>().SetFloat("speed" , 0.2f);
        }
        else
        {
            walking = false;
            GetComponentInChildren<Animator>().SetFloat("speed", 0.0f);
            if (footsteps != null && footsteps.isPlaying)
                footsteps.StopSoundLooped();
        }
        if (!player.alive)
        {
            targetPosition.transform.position = player.transform.position;
            agent.destination = targetPosition.transform.position;
            return;
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
