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


    private Quaternion inputRotation, RotationTargetPhase0 = new Quaternion(0.0f,0f,0f,1f);
    public float marginError;

    private void Update()
    {
        inputRotation = Quaternion.Euler(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0));

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
        }
        

    }

}
