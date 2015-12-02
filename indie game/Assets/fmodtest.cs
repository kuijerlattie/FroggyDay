using UnityEngine;
using System.Collections;

public class fmodtest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var studioSystem = FMODUnity.RuntimeManager.StudioSystem;
        FMOD.Studio.CPU_USAGE cpuUsage;
        studioSystem.getCPUUsage(out cpuUsage);

        var lowlevelSystem = FMODUnity.RuntimeManager.LowlevelSystem;
        uint version;
        lowlevelSystem.getVersion(out version);
    }
	
	// Update is called once per frame
	void Update () {
        
            
            
        
	}
}
