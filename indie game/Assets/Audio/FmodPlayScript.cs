using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
public class FmodPlayScript : MonoBehaviour {

    FMODUnity.StudioEventEmitter emitter;

	void Start () {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
	}
	
    public void PlaySound(string eventName)
    {
        if(emitter == null)
        {
            emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        }
        Debug.Log(emitter.Event);
        emitter.Event = eventName;
        emitter.Play();
        StartCoroutine(StopSound());
    }

    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(5);
        GameObject.Destroy(gameObject);
    }

}
