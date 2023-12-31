using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectRotation : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float maxDelta;

    public void TryToReachTargetRotation(Quaternion targetRotation)
    {
        //transform.Rotate(new Vector3(0,0,targetRotation.eulerAngles.x * rotSpeed), Space.World);
        transform.rotation = targetRotation;
    }
    public void TryToReachTargetRotationMouse(Quaternion targetRotation)
    {
        transform.Rotate(new Vector3(0,0,targetRotation.eulerAngles.x * rotSpeed), Space.World);
    }
}
