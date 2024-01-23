using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
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
    #endregion

    public AudioSource[] Jingles;



}
