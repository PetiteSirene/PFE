using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // SINGLETON PART
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static SoundManager instance;
    //

    public AudioSource[] jingles;
    public AudioSource[] ambiences;
    public AudioSource[] soundEffects;

}
