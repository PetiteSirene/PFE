using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectRotation : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float maxDelta;
    private Quaternion initRotConj = Quaternion.identity;

    public void InitRotation(Quaternion quat)
    {
        initRotConj = Quaternion.Inverse(quat);

    }

    public void SetRotation(Quaternion quat)
    {
        transform.rotation = quat * initRotConj;
    }
    public void AddRotation(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }

}
