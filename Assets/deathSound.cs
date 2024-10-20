using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class deathSound : MonoBehaviour
{
    public EventInstance JammoDying;
    // Start is called before the first frame update
    void PlayTooBadSound()
    {
        JammoDying = RuntimeManager.CreateInstance("event:/SFX/SFX_Death");
        JammoDying.start();
        JammoDying.release();
    }
 
}
