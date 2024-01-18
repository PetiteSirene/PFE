using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectBall : MonoBehaviour
{
    [SerializeField] 
    private float minHeight;
    private int labyPhase = 1;
    [SerializeField]
    private float gravityScale = 50;
    [SerializeField]
    private Rigidbody rigidbodyy;

    // Update is called once per frame
    void Update()
    {
        CorrectForce();
        TryWinLaby();
    }

    void TryWinLaby()
    {
        if (StateManager.Instance.CurrentPhase == labyPhase && gameObject.transform.position.y < minHeight)
        {
            //StateManager.Instance.AchievePhase(labyPhase);
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
}
