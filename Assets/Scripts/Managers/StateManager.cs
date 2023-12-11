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
        rightCloud = GameObject.Find("RightGroup").gameObject;
        leftCloud = GameObject.Find("LeftGroup").gameObject;
        TheVRObject.Instance.transform.position = new Vector3(0,0,200);
    }

    public void AchievePhase(int i)
    {
        if (currentPhase == i)
        {
            StartCoroutine(ToDoAtEndPhase(i));

        }
    }

    private IEnumerator ToDoAtEndPhase(int i)
    {
        if (currentPhase < maxPhase)
        {
            currentPhase++;
            Debug.Log("nouvelle phase : "+ currentPhase);
        }
        else
        {
            Debug.Log("dernière phase du jeu : " + currentPhase);
        }

        switch (i)
        {
            case 0:
                float durationTransition = 2.0f;
                float elapsedTime = 0.0f;
                Vector3 startPos = TheVRObject.Instance.transform.localPosition;
                Vector3 endPos = new Vector3(0.00319828838f, 277.717926f, 155);

                while (elapsedTime < durationTransition)
                {
                    TheVRObject.Instance.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / durationTransition);
                    elapsedTime += Time.deltaTime;
                    rightCloud.transform.Translate(Vector3.right * 1);
                    leftCloud.transform.Translate(Vector3.left * 1);
                    yield return null;
                }
                rightCloud.SetActive(false);
                leftCloud.SetActive(false);
                break;

            case 1:
                break;
            case 2:
                break;
            default:
                Debug.Log("Erreur de fin de phase");
                break;
        }
        
    }

}
