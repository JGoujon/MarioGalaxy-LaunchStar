using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public string playerLand;
    public string playerWalk;
    private bool isWalking = false;
    private CharacterController characterController;

    public void Player_Land()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerLand, transform.position);
    }
    public void Player_Walk()
    {
        if (isWalking && characterController.isGrounded) 
        { 
            FMODUnity.RuntimeManager.PlayOneShot(playerWalk, transform.position);
        }
        
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
