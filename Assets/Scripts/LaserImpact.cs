using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserImpact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Lamp>().IsUnityNull())
        {
            other.GetComponent<Lamp>().Lit();
        }
    }
}
