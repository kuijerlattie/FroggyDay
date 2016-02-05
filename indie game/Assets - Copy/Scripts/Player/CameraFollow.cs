using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    Transform player;
    [SerializeField]
    Transform mainCameraTransform;  //needed for rotation

    private const float _SCALE = 400.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temprotation = Quaternion.ToEulerAngles(mainCameraTransform.rotation);
        gameObject.transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        gameObject.transform.localRotation = Quaternion.Euler(90, temprotation.y * (180.0f/Mathf.PI), 0);

        Vector2 pos = new Vector2(Screen.width - 250, Screen.height - 250);
        GetComponent<Camera>().pixelRect = new Rect(pos, new Vector2(_SCALE, _SCALE));
    }
}
