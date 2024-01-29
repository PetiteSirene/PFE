using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class VRObjectBall : MonoBehaviour
{
    [SerializeField] 
    private float minHeight;
    private int labyPhase = 1;
    [SerializeField]
    private float gravityScale = 50;
    [SerializeField]
    private Rigidbody rigidbodyy;
    public bool labyReady = false;

    // Update is called once per frame
    void Update()
    {
        CorrectForce();
        TryWinLaby();
    }

    void TryWinLaby()
    {
        if (labyReady && StateManager.Instance.CurrentPhase == labyPhase && gameObject.transform.position.y < minHeight)
        {
            TheVRObject.Instance.WinLaby();
            Destroy(this);
        }
    }

    void CorrectForce()
    {
        rigidbodyy.AddForce(gravityScale * Vector3.down, ForceMode.Force);
        if (rigidbodyy.velocity.y > 0)
        {
            rigidbodyy.AddForce(rigidbodyy.velocity.y * Vector3.down, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<LabyEnd>().IsUnityNull())
        {
                transform.SetParent(null); 
        }
    }
}
