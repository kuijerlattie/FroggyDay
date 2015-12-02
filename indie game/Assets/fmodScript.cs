using UnityEngine;
using System.Collections;

public class fmodScript : MonoBehaviour {


	FMOD.Studio.EventInstance noremorse; // declare that you're using an event, and give it a name.
	FMOD.Studio.ParameterInstance buildup; // declare that you're using a parameter, and give it a name.

	
	GameObject cube7; // define a gameobject
	
	void Start () {

		noremorse = FMOD_StudioSystem.instance.GetEvent ("event:/noremorse"); // tell it which event actually is the one you declared above
		noremorse.getParameter ("buildup", out buildup); // tell it which parameter actually is the one you declared above

		cube7 = GameObject.Find ("cube7"); // find game object cube7
	

	}


	void OnTriggerEnter(Collider other) { // when the gameobject this script is attached to, makes a collision with other then.... 

		if (other.name == "cube8") { // if other is cube8
			noremorse.start(); // play event noremorse
			buildup.setValue (3.03f); // give parameter buildup a specific float value
		}

	}

	void OnTriggerExit(Collider other) { 
		
		if (other.name == "cube9") { // is other is cube 9
			bass.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // stop the event from playing IMMEDIATELY
			
		}
	}

	void Update() {

		if (Input.GetKeyUp ("1")) { // when you release the key 1, then 
			noremorse.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // stop the event from playing but do allow fadeouts from envelopes (ADSR) or DSP processing, like the tail of a reverb 
			
			
		}
		
		if (Input.GetKeyDown ("1")) { // when you press down 1 then...
			noremorse.start(); // play the event
			
		}
		
		float distance = Vector3.Distance (this.gameObject.transform.position, cube7.transform.position); // there is a float value, which is the outcome of the function Vector3.Distance, which is the distance between the object this script is attached to and the object named after.
		float increasingdistance = 0 + Mathf.Clamp (distance / 20.0f, 0, 1); // there is a float which is the result of the equation 0+ blablabla 
		float decreasingdistance = 1 - Mathf.Clamp (distance / 20.0f, 0, 1); // there is a float which is the result of the equation 1- blablabla

		buildup.setValue (increasingdistance); // set the parameter to the same value as the float above
		buildup.setValue (decreasingdistance); // set the parameter to the same value as the float above

	}
}
