using UnityEngine;
using System.Collections;

public class VisionAssistant : MonoBehaviour {

    RaycastHit hit;
    GameObject player;
    GameObject disabledWall;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (disabledWall != null)
        {
            disabledWall.SetActive(true);
            disabledWall = null;
        }
        LayerMask layermask = (1 << 4);
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(player.transform.position), out hit, 100, layermask))
        if (Physics.Raycast(Camera.main.transform.position, (player.transform.position - Camera.main.transform.position), out hit, 100, layermask))
        {
            Debug.Log("wall spotted");
            hit.collider.gameObject.transform.parent.gameObject.SetActive(false);
            disabledWall = hit.collider.gameObject.transform.parent.gameObject;
        }
	}
}
