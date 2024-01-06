using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            TheVRObject.Instance.serialHandler.SendBallOut();
        }
    }
}
