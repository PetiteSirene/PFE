using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    [SerializeField] private List<Face> faces;
    [SerializeField] private bool enabled = false;
    [SerializeField] private float startTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        foreach(Face face in faces)
        {
            face.Initialize(pos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            float x = 0.5f - 0.5f * Mathf.Cos(Time.time - startTime);
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            foreach(Face face in faces)
            {
                face.Lerp(x, pos, rot);
            }
        }
        
    }

    public void Enable()
    {
        startTime = Time.time;
        enabled = true;
    }
}
