using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    [SerializeField] private List<Face> faces;

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
        float p = 0.5f + 0.5f * Mathf.Sin(Time.time);
        foreach(Face face in faces)
        {
            face.Lerp(p);
        }
    }
}
