using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectBall : MonoBehaviour
{
    [SerializeField] private float minHeight;
    private int labyPhase = 1;

    // Update is called once per frame
    void Update()
    {
        TryWinLaby();
    }

    void TryWinLaby()
    {
        if (StateManager.Instance.currentPhase == labyPhase && gameObject.transform.position.y < minHeight)
        {
            //StateManager.Instance.AchievePhase(labyPhase);
            Destroy(this);
        }
    }
}
