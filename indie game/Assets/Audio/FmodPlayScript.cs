using UnityEngine;
using System.Collections;
using FMOD.Studio;

[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
public class FmodPlayScript : MonoBehaviour {

    public bool isPlaying = false;
    public FMODUnity.StudioEventEmitter emitter;

	void Start () {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        
    }
	
    public void PlaySound(string eventName)
    {
        if(emitter == null)
        {
            emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        }
        isPlaying = true;
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
        isPlaying = true;
        emitter.TriggerOnce = false;
        emitter.Event = eventName;
        emitter.Play();
        return emitter;
    }

    public void StopSoundLooped()
    {
        isPlaying = false;
        emitter.Stop();
        GameObject.Destroy(gameObject);
    }

    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(5);
        isPlaying = false;
        emitter.Stop();
        GameObject.Destroy(gameObject);
    }

}
