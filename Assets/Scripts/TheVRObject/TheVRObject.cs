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

                vrObjectRotation.SetRotation(inputRotation);//*Quaternion.Inverse(initialRotation));
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
                vrObjectRotation.SetRotation(inputRotation); //*Quaternion.Inverse(initialRotation));
                //Debug.Log(inputRotation);
            }
        }
        else
        {
            if (StateManager.Instance.currentPhase == 0)
            {
                vrObjectRotation.AddRotation(GetInputDir());
                if (Vector3.Distance(inputRotation.eulerAngles, RotationTargetPhase0.eulerAngles) <= marginError)
                {
                    StateManager.Instance.AchievePhase(0);
                }

            }

            if (StateManager.Instance.currentPhase == 1)
            {
                vrObjectRotation.AddRotation(GetInputDir());
                //Debug.Log(inputRotation);
            }
        }
        
        
        

    }

    private Vector3 GetInputDir()
    {
        float x, y, z;
        if (Input.GetKey(KeyCode.Q))
        {
            x = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else
        {
            x = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            y = 1;
        }
        else
        {
            y = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            z = -1;
        }
        else if(Input.GetKey(KeyCode.E))
        {
            z = 1;
        }
        else
        {
            z = 0;
        }
        return new Vector3(x,y,z);
    }

}
