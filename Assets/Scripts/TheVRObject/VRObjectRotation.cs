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

    public Quaternion GetRealRot(Quaternion quat)
    {
        return quat * initRotConj;
    }

    private Quaternion GetRotDiff(Quaternion quat1, Quaternion quat2)
    {
        return quat1 * Quaternion.Inverse(quat2);
    }

    public float GetAngle(Quaternion quat1, Vector3 targetRot)
    {
        Quaternion quat = GetRealRot(quat1);
        float angle = Quaternion.Angle(quat, Quaternion.Euler(targetRot));
        return angle;
    }

}
