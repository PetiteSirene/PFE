using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectLaser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private float laserRange;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        CastLaser();
    }

    private void CastLaser()
    {
        Vector3 initialPos = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(initialPos, transform.forward, out hit, laserRange))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, initialPos);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
