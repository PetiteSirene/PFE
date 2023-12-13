using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] private float intensity;
    private Vector3 posClose;
    private Vector3 posOpen;


    public void Initialize(Vector3 posCenter)
    {
        Vector3 pos = transform.position;
        posClose = pos;
        posOpen = pos + intensity * (pos - posCenter);   
    }

    public void Lerp (float p)  // 0 <= p <= 1;
    {
        transform.position = Vector3.Lerp(posClose, posOpen, p);
    }

    
}
