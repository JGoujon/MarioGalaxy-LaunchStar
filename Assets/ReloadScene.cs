using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public Animator fadeAnimator;  // Référence à l'Animator
    public float fadeDuration = 6f; // Durée de l'animation de fade
    public Animator tooBadAnimator; // Référence à l'Animator pour le texte "Too Bad !"
    public ParticleSystem smokeEffect; // Référence au système de particules pour l'effet de fumée
    public Canvas canvas;
    public Canvas canvasTooBad;

    private void Start()
    {
        canvas.enabled = false;
        canvasTooBad.enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger.");  // Log pour vérifier l'entrée du joueur
            if (fadeAnimator != null && fadeAnimator.isActiveAndEnabled)
            {
                canvas.enabled = !canvas.enabled;
                canvasTooBad.enabled = !canvasTooBad.enabled;
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
        Debug.Log("Fade animation triggered.");  // Log pour vérifier le déclenchement de l'animation
        fadeAnimator.SetTrigger("Fade");  // Déclencher l'animation de fade
        tooBadAnimator.SetTrigger("PlayTooBad");
        smokeEffect.Play();
        yield return new WaitForSeconds(fadeDuration); // Attendre la fin de l'animation
        //canvasGroup.alpha = 1;
        Debug.Log("Scene is reloading.");  // Log juste avant de recharger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}