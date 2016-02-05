using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

    public float degreesPerSecond;
    public bool yinsteadofz = false;
	// Update is called once per frame
	void Update () {
        if (yinsteadofz)
        {
            transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
        }
        else {
            transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
        }
    }
}
