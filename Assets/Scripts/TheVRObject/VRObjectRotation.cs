using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectRotation : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float maxDelta;

    public void SetRotation(Quaternion quat)
    {
        //transform.Rotate(new Vector3(0,0,targetRotation.eulerAngles.x * rotSpeed), Space.World);
        transform.rotation = quat;
    }
    public void AddRotation(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }
}
