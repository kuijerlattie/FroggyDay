using UnityEngine;
using System.Collections;
using FMOD.Studio;

[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
public class FmodPlayScript : MonoBehaviour {

    public bool isPlaying = false;
    public FMODUnity.StudioEventEmitter emitter;
    int seconds = 5;

	void Start () {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        
    }
	
    public void PlaySound(string eventName, int pseconds = 4)
    {
        seconds = pseconds;
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
        emitter.AllowFadeout = true;
        emitter.Play();
        return emitter;
    }

    public void StopSoundLooped()
    {
        StartCoroutine(StopSoundloop());
    }

    private IEnumerator StopSoundloop()
    {
        yield return new WaitForSeconds(1);
        isPlaying = false;
        emitter.Stop();
        GameObject.Destroy(gameObject);
    }

    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(seconds);
        isPlaying = false;
        emitter.Stop();
        GameObject.Destroy(gameObject);
    }

}
