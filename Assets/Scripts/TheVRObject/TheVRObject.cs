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

    [SerializeField] private LaserTarget laserTarget;

    private FaceController faceController;
    public FaceController FaceController => faceController;

    [SerializeField] Vector3 targetRotation;
    [SerializeField] private float initTimer;

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
        faceController = GetComponent<FaceController>();
    }

    private Quaternion inputRotation;
    public float marginError;
    private float timeAtTarget;

    private void Update()
    {
        initTimer += Time.deltaTime;
        if (!serialHandler.ArduinoNotConnected)
        {
            inputRotation = serialHandler.ReceivedQuaternion;
            if (Input.GetKeyDown(KeyCode.I))
            {
                vrObjectRotation.InitRotation(inputRotation);
                if (StateManager.Instance.CurrentPhase == 0)
                {
                    serialHandler.SendMessage("b");
                }

            }
            if (StateManager.Instance.CurrentPhase == 0)
            {

                vrObjectRotation.SetRotation(inputRotation);
                serialHandler.SendAngularDifference(vrObjectRotation.GetAngle(inputRotation, targetRotation));
                
                if (vrObjectRotation.GetAngle(inputRotation, targetRotation) <= marginError && initTimer > 3f)
                {
                    timeAtTarget += Time.deltaTime;
                    if (timeAtTarget > 1f)
                    {
                        WinGyro();
                    }
                }
                else 
                {
                    timeAtTarget = 0;
                }


            }
            else if (StateManager.Instance.CurrentPhase == 1)
            {
                vrObjectRotation.SetRotation(inputRotation);
            }
            else if (StateManager.Instance.CurrentPhase == 2)
            {
                Quaternion realRot = vrObjectRotation.GetRealRot(inputRotation);
                laserTarget.Move(realRot.eulerAngles);
                vrObjectRotation.InitRotation(inputRotation);
            }
        }
        else
        {
            if (StateManager.Instance.CurrentPhase == 0)
            {
                vrObjectRotation.AddRotation(GetInputDir());
                if (vrObjectRotation.GetAngle(inputRotation, targetRotation) <= marginError)
                {
                    timeAtTarget += Time.deltaTime;
                    if (timeAtTarget > 0.5f)
                    {
                        StateManager.Instance.AchievePhase(0);
                    }
                }
                else 
                {
                    timeAtTarget = 0;
                }

            }

            if (StateManager.Instance.CurrentPhase == 1)
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

    public void WinGyro()
    {
        StateManager.Instance.AchievePhase(0);
        serialHandler.SendMessage("c");
    }

    public void WinLaby()
    {
        StateManager.Instance.AchievePhase(1);

    }

}
