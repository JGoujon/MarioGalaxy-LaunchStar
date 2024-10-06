using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public string playerLand;
    public string playerWalk;
    public void Player_Land()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerLand, transform.position);
    }
    public void Player_Walk()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerWalk, transform.position);
    }
}
