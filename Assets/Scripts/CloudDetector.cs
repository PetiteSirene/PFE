using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            TheVRObject.Instance.WinLaby();
        }
    }
}
