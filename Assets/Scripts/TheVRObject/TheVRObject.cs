using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheVRObject : MonoBehaviour
{
    // SINGLETON PART
    private static TheVRObject instance = null;
    public static TheVRObject Instance => instance;
    //

    private VRObjectRotation vrObjectRotation;
    private StateManager stateManager;
    //private Quaternion initialRotation;

    [SerializeField] Quaternion targetRotation;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);   
        LinkComponents();
    }

    private void LinkComponents()
    {
        stateManager = GameObject.Find("SuperManager").GetComponent<StateManager>();
        vrObjectRotation = GetComponent<VRObjectRotation>();
    }

    /*private void Start()
    {
        initialRotation = GetComponent<SerialHandler>().ReceivedQuaternion;
    }*/


    private Quaternion inputRotation, RotationTargetPhase0 = new Quaternion(0.0f,0f,0f,1f);
    public float marginError;

    private void Update()
    {
        inputRotation = GetComponent<SerialHandler>().ReceivedQuaternion;

        if (stateManager.currentPhase == 0)
        {
            if (Vector3.Distance(inputRotation.eulerAngles, RotationTargetPhase0.eulerAngles) <= marginError)
            {
                stateManager.AchievePhase(0);
            }

        }

        if (stateManager.currentPhase == 1)
        {
            vrObjectRotation.TryToReachTargetRotation(inputRotation);
            //Debug.Log(inputRotation);
        }
        

    }

}
