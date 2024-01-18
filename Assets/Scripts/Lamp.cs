using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> fireVFXs;
    private bool isLit = false;
    public bool IsLit => isLit;

    public void Lit()
    {
        foreach(ParticleSystem vfx in fireVFXs)
        {
            vfx.Play();
        }
        isLit = true;
        StateManager.Instance.CheckLamps();
    }

    


}
