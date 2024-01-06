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
    public SerialHandler serialHandler;
    private Quaternion initialRotation;
    private bool initialRotationSaved;

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

    private Quaternion inputRotation;
    private Quaternion RotationTargetPhase0 = Quaternion.Euler( 0.09080228f, -0.05250352f, 0.3077305f);
    public float marginError;

    private void Update()
    {
        if (!serialHandler.ArduinoNotConnected)
        {
            inputRotation = serialHandler.ReceivedQuaternion;
            if (StateManager.Instance.currentPhase == 0)
            {
                if (!initialRotationSaved)
                {
                    initialRotation = GetComponent<SerialHandler>().ReceivedQuaternion;
                }

                vrObjectRotation.TryToReachTargetRotation(inputRotation);//*Quaternion.Inverse(initialRotation));
                /*TODO: Reactivate SendAngularDifference() for later work*/
                //serialHandler.SendAngularDifference(Quaternion.Angle(inputRotation, RotationTargetPhase0));
                //Debug.Log(Quaternion.Angle(inputRotation, RotationTargetPhase0));



                if (Quaternion.Angle(inputRotation, RotationTargetPhase0) <= marginError)
                {
                    StateManager.Instance.AchievePhase(0);
                }

            }

            if (StateManager.Instance.currentPhase == 1)
            {
                vrObjectRotation.TryToReachTargetRotation(inputRotation); //*Quaternion.Inverse(initialRotation));
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
