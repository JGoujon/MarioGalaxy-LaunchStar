using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem smokeEffect;
    void PlaySmokeEffect()
    {
        smokeEffect.Play();
    }
}
