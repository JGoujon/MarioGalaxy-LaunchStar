using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public Animator fadeAnimator;  // R�f�rence � l'Animator
    public float fadeDuration = 6f; // Dur�e de l'animation de fade
    public Canvas canvas;

    private void Start()
    {
        canvas.enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger.");  // Log pour v�rifier l'entr�e du joueur
            if (fadeAnimator != null && fadeAnimator.isActiveAndEnabled)
            {
                canvas.enabled = !canvas.enabled;
                StartCoroutine(ReloadAfterFade());
            }
            else
            {
                Debug.Log("Fade Animator is not properly set up.");
            }
        }
    }

    private IEnumerator ReloadAfterFade()
    {
        Debug.Log("Fade animation triggered.");  // Log pour v�rifier le d�clenchement de l'animation
        fadeAnimator.SetTrigger("Fade");  // D�clencher l'animation de fade
        yield return new WaitForSeconds(fadeDuration); // Attendre la fin de l'animation
        //canvasGroup.alpha = 1;
        Debug.Log("Scene is reloading.");  // Log juste avant de recharger la sc�ne
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}