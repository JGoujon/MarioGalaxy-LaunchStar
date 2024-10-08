using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public string playerLand;
    public string playerWalk;
    private bool isWalking = false;
    public void Player_Land()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerLand, transform.position);
    }
    public void Player_Walk()
    {
        if (isWalking) 
        { 
            FMODUnity.RuntimeManager.PlayOneShot(playerWalk, transform.position);
        }
        
    }
    void Update()
    {
        // Check if player is moving
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
