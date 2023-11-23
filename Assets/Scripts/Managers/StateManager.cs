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

    private TheVRObject theVRObject;

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

    private void Start()
    {
        theVRObject = GameObject.Find("TheVRObject").GetComponent<TheVRObject>();
        theVRObject.transform.position = new Vector3(0,0,200);
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
        StartCoroutine(TransitionFinPhase0());
    }

    private void ToDoAtEndPhase1()
    {
        Debug.Log("fin de la phase 1");
    }

    private void ToDoAtEndPhase2()
    {
        Debug.Log("fin de la phase 2");
    }

    private IEnumerator TransitionFinPhase0()
    {
        float durationTransition = 2.0f;
        float elapsedTime = 0.0f;
        Vector3 startPos = theVRObject.transform.localPosition;
        Vector3 endPos = new Vector3(0.00319828838f, 277.717926f, 155);

        while (elapsedTime < durationTransition)
        {
            theVRObject.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / durationTransition);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
    }


    private void Update()
    {
        Debug.Log(currentPhase);
    }


}
