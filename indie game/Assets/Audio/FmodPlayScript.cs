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
        emitter.Event = eventName;
        emitter.Play();
        StartCoroutine(StopSound());
    }

    public FMODUnity.StudioEventEmitter PlaySoundLooped(string eventName)
    {
        if (emitter == null)
        {
            emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        }
        emitter.TriggerOnce = false;
        emitter.Event = eventName;
        emitter.Play();
        return emitter;
    }

    public void StopSoundLooped(FMODUnity.StudioEventEmitter emitter)
    {
        if (emitter == null)
            return;
        GameObject.Destroy(emitter.gameObject);
    }

    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(5);
        GameObject.Destroy(gameObject);
    }

}
