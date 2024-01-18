using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> fireVFXs;


    public void Lit()
    {
        foreach(ParticleSystem vfx in fireVFXs)
        {
            vfx.Play();
        }
    }


}
