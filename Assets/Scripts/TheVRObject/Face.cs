using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] private float intensity;
    [SerializeField]private Vector3 posClose;
    [SerializeField]private Vector3 posOpen;


    public void Initialize(Vector3 posCenter)
    {
        Vector3 pos = transform.position;
        posClose = pos - posCenter;
        posOpen = posClose * (1 + intensity);   
    }

    public void Lerp (float p, Vector3 pos, Quaternion rot)  // 0 <= p <= 1;
    {
        transform.position = Vector3.Lerp(pos + rot * posClose, pos + rot * posOpen, p);
    }

    
}
