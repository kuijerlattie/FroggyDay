using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

    public float degreesPerSecond;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }
}
