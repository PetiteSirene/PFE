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


    private Vector3 inputRotation;

    private void Update()
    {
        if(stateManager.currentPhase == 1)
        {
            inputRotation = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            vrObjectRotation.TryToReachTargetRotation(Quaternion.Euler(inputRotation));
        }
        

    }

}
