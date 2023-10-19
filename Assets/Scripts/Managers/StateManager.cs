using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // SINGLETON PART
    private static StateManager instance = null;
    public static StateManager Instance => instance;
    //
    
    private int currentPhase = 0;
    [SerializeField] private int maxPhase;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);   
    }

    public void AchievePhase(int i)
    {
        if (currentPhase == i)
        {
            Invoke("ToDoAtEndPhase"+currentPhase, 0);
            if (currentPhase < maxPhase)
            {
                currentPhase ++;

            }
            else
            {
                Debug.Log("dernière phase du jeu : " + currentPhase);
            }
        }
    }

    private void ToDoAtEndPhase0()
    {
        Debug.Log("fin de la phase 0");
    }

    private void ToDoAtEndPhase1()
    {
        Debug.Log("fin de la phase 1");
    }

    private void ToDoAtEndPhase2()
    {
        Debug.Log("fin de la phase 2");
    }





}
