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
        if (Input.GetKeyDown(KeyCode.H))    //TODO remove this in final build
        {
            Scroll scroll = new Scroll();
            scroll.SetSpell(Scroll.Spells.W);
            scroll.Drop(new Vector3(0, -1.8f, 260));

            Scroll scroll2 = new Scroll();
            scroll2.SetSpell(Scroll.Spells.E);
            scroll2.Drop(new Vector3(0, -1.8f, 260));

            Scroll scroll3 = new Scroll();
            scroll3.SetSpell(Scroll.Spells.R);
            scroll3.Drop(new Vector3(0, -1.8f, 260));
        }
        if (agent.velocity.magnitude > 0)
        {

            if (footsteps == null || !footsteps.isPlaying)
            {
                footsteps = GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.Footstep);
            }
            
            walking = true;
            GetComponentInChildren<Animator>().SetBool("walk" , true);
        }
        else
        {
            walking = false;
            GetComponentInChildren<Animator>().SetBool("walk", false);
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
