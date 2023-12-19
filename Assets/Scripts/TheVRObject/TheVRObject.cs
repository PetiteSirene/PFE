using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheVRObject : MonoBehaviour
{
    // SINGLETON PART
    private static TheVRObject instance = null;
    public static TheVRObject Instance => instance;

    private VRObjectRotation vrObjectRotation;
    private SerialHandler serialHandler;
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
        vrObjectRotation = GetComponent<VRObjectRotation>();
        serialHandler = GetComponent<SerialHandler>();
    }

    /*private void Start()
    {
        initialRotation = GetComponent<SerialHandler>().ReceivedQuaternion;
    }*/


    private Quaternion inputRotation;
    private Quaternion RotationTargetPhase0 = new Quaternion(0.2f,0f,0f,1f);
    public float marginError;

    private void Update()
    {
        if (!serialHandler.ArduinoNotConnected)
        {
            inputRotation = serialHandler.ReceivedQuaternion;
            if (StateManager.Instance.currentPhase == 0)
            {
                vrObjectRotation.TryToReachTargetRotation(inputRotation);
                serialHandler.SendAngularDifference(Quaternion.Angle(inputRotation, RotationTargetPhase0));
                if (Vector3.Distance(inputRotation.eulerAngles, RotationTargetPhase0.eulerAngles) <= marginError)
                {
                    StateManager.Instance.AchievePhase(0);
                }

            }

            if (StateManager.Instance.currentPhase == 1)
            {
                vrObjectRotation.TryToReachTargetRotation(inputRotation);
                //Debug.Log(inputRotation);
            }
        }
        else
        {
            inputRotation = Quaternion.Euler(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0));
            if (StateManager.Instance.currentPhase == 0)
            {
                vrObjectRotation.TryToReachTargetRotationMouse(inputRotation);
                if (Vector3.Distance(inputRotation.eulerAngles, RotationTargetPhase0.eulerAngles) <= marginError)
                {
                    StateManager.Instance.AchievePhase(0);
                }

            }

            if (StateManager.Instance.currentPhase == 1)
            {
                vrObjectRotation.TryToReachTargetRotationMouse(inputRotation);
                //Debug.Log(inputRotation);
            }
        }
        
        
        

    }

}
