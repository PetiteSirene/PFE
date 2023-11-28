using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // SINGLETON PART
    private static StateManager instance = null;
    public static StateManager Instance => instance;
    //
    
    public int currentPhase = 0;
    [SerializeField] private int maxPhase;

    private TheVRObject theVRObject;
    private GameObject rightCloud, leftCloud;

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
        rightCloud = GameObject.Find("RightGroup").gameObject;
        leftCloud = GameObject.Find("LeftGroup").gameObject;
        theVRObject.transform.position = new Vector3(0,0,200);
    }

    public void AchievePhase(int i)
    {
        if (currentPhase == i)
        {
            StartCoroutine(TransitionFinPhase(i));

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

    private IEnumerator TransitionFinPhase(int i)
    {
        switch (i)
        {
            case 0:
                float durationTransition = 2.0f;
                float elapsedTime = 0.0f;
                Vector3 startPos = theVRObject.transform.localPosition;
                Vector3 endPos = new Vector3(0.00319828838f, 277.717926f, 155);

                while (elapsedTime < durationTransition)
                {
                    theVRObject.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / durationTransition);
                    elapsedTime += Time.deltaTime;
                    rightCloud.transform.Translate(Vector3.right * 1);
                    leftCloud.transform.Translate(Vector3.left * 1);
                    yield return null;
                }
                rightCloud.SetActive(false);
                leftCloud.SetActive(false);

                //Fin de coroutine
                Invoke("ToDoAtEndPhase" + currentPhase, 0);
                if (currentPhase < maxPhase)
                {
                    currentPhase++;

                }
                else
                {
                    Debug.Log("dernière phase du jeu : " + currentPhase);
                }
                break;

            case 1:
                break;
            default:
                Debug.Log("Erreur de fin de phase");
                break;
        }
        
    }


    private void Update()
    {
        //Debug.Log(currentPhase);
    }


}
