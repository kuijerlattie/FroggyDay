using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    Transform player;
    [SerializeField]
    Transform mainCameraTransform;  //needed for rotation
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temprotation = Quaternion.ToEulerAngles(mainCameraTransform.rotation);
        //Debug.Log(mainCameraTransform.rotation.ToEuler().y);
        gameObject.transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        gameObject.transform.localRotation = Quaternion.Euler(90, temprotation.y * (180.0f/3.14f), 0);
        //int ypos = Screen.height - 10;
       // int width = Screen.width - 10;
        Vector2 pos = new Vector2(Screen.width - 250, Screen.height - 250);
        int scale = 400;
        GetComponent<Camera>().pixelRect = new Rect(pos, new Vector2(scale, scale));
    }
}
