using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public Animator fadeAnimator;  // R�f�rence � l'Animator
    public float fadeDuration = 6f; // Dur�e de l'animation de fade
    public Animator tooBadAnimator; // R�f�rence � l'Animator pour le texte "Too Bad !"
    public ParticleSystem smokeEffect; // R�f�rence au syst�me de particules pour l'effet de fum�e
    public Canvas canvas;
    public Canvas canvasTooBad;
    public Canvas canvasSmoke;
    public string music;
    public string snapshot;
    public FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance  muteaudio;

    private void Start()
    {
        canvas.enabled = false;
        canvasTooBad.enabled = false;
        canvasSmoke.enabled = false;

        instance = FMODUnity.RuntimeManager.CreateInstance(music);
        instance.start();

        muteaudio = FMODUnity.RuntimeManager.CreateInstance(snapshot);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger.");  // Log pour v�rifier l'entr�e du joueur
            if (fadeAnimator != null && fadeAnimator.isActiveAndEnabled)
            {
                canvas.enabled = !canvas.enabled;
                canvasTooBad.enabled = !canvasTooBad.enabled;
                canvasSmoke.enabled= !canvasSmoke.enabled;
                StartCoroutine(ReloadAfterFade());
            }
            else
            {
                Debug.Log("Fade Animator is not properly set up.");
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Stop the snapshot to restore normal audio when the application gains focus
            muteaudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            // Start the snapshot to mute audio when the application loses focus
            muteaudio.start();
        }
    }

    private IEnumerator ReloadAfterFade()
    {
        Debug.Log("Fade animation triggered.");  // Log pour v�rifier le d�clenchement de l'animation
        fadeAnimator.SetTrigger("Fade");  // D�clencher l'animation de fade
        tooBadAnimator.SetTrigger("PlayTooBad");
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //smokeEffect.Play();
        yield return new WaitForSeconds(fadeDuration); // Attendre la fin de l'animation
        //canvasGroup.alpha = 1;
        Debug.Log("Scene is reloading.");  // Log juste avant de recharger la sc�ne
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        instance.release();
        muteaudio.release();
    }
}