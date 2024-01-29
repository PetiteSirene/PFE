using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectLaser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private float laserRange;
    [SerializeField] private GameObject impactPoint;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private LaserTarget laserTarget;
    public bool isActive = false;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(isActive)
        {
            CastLaser();
        }
    }

    private void CastLaser()
    {
        Vector3 initialPos = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(initialPos, laserTarget.transform.position - initialPos, out hit, laserRange, collisionLayer))
        {
            impactPoint.transform.position = hit.point;
            impactPoint.SetActive(true);
            lineRenderer.SetPosition(0, initialPos);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            impactPoint.SetActive(false);
            lineRenderer.SetPosition(0, initialPos);
            lineRenderer.SetPosition(1, initialPos + laserRange * (laserTarget.transform.position - initialPos));
        }
    }
}
