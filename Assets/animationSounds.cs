using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public string playerLand;
    public void PlayFMODSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerLand, transform.position);
    }
}
