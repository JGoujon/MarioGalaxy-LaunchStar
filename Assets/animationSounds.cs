using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSounds : MonoBehaviour
{
    public string playerLand;
    public string playerWalk;
    public string playerTalk;
    public string playerMove;

    private bool isWalking = false;

    private float idleTime = 0f;
    private float idleThreshold = 10f; // Temps d'attente avant les vocalisations
    private CharacterController characterController;

    private FMOD.Studio.EventInstance talkEventInstance; // Pour contr�ler l'arr�t du son de vocalisation

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        talkEventInstance = FMODUnity.RuntimeManager.CreateInstance(playerTalk);
    }

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
    public void Player_Move()
    {
        if (isWalking && characterController.isGrounded)
        {
            FMODUnity.RuntimeManager.PlayOneShot(playerMove, transform.position);
        }
    }

    public void Player_Talk()
    {
        // Si le joueur est immobile et au sol depuis plus de 10 secondes, on joue le son de "talk"
        if (idleTime >= idleThreshold && !isWalking)
        {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            talkEventInstance.getPlaybackState(out playbackState);

            if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                talkEventInstance.start(); // D�marre le son de vocalisation
            }
        }
    }


    void Update()
    {
        // V�rifie si le joueur est en train de marcher
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && characterController.isGrounded)
        {
            isWalking = true;
            idleTime = 0f; // Reset le temps d'immobilit� lorsque le joueur commence � bouger

            // Arr�te le son de vocalisation si le joueur bouge
            FMOD.Studio.PLAYBACK_STATE playbackState;
            talkEventInstance.getPlaybackState(out playbackState);

            if (playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                talkEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
        else if (characterController.isGrounded)
        {
            isWalking = false;
            idleTime += Time.deltaTime; // Incr�mente le temps d'immobilit�

            // V�rifie si le joueur doit vocaliser apr�s �tre rest� immobile
            Player_Talk();
        }

    }

    void OnDestroy()
    {
        // S'assurer que l'instance de talk est bien lib�r�e quand l'objet est d�truit
        talkEventInstance.release();
    }
}