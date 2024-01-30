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
    public int CurrentPhase => currentPhase;
    private int laserPhase = 2;
    private int maxPhase = 3;
    

    private GameObject rightCloud, leftCloud;
    private Transform VRObjectTransform;
    [SerializeField] private VRObjectBall ball;
    [SerializeField] private VRObjectLaser laser;
    [SerializeField] private List<Lamp> lamps;

    public bool clouds = true;

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
        if (clouds)
        {
            rightCloud = GameObject.Find("RightGroup").gameObject;
            leftCloud = GameObject.Find("LeftGroup").gameObject;
        }
        VRObjectTransform = TheVRObject.Instance.transform;
    }

    public void AchievePhase(int i)
    {
        if (currentPhase == i)
        {
            StartCoroutine(ToDoAtEndPhase(i));
            if (SoundManager.Instance.Jingles[i] == null)
                return;
            SoundManager.Instance.Jingles[i].Play();

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
                    Vector3 startPos = VRObjectTransform.localPosition;
                    Vector3 endPos = new Vector3(0f, 300f, 200f);

                    while (elapsedTime < durationTransition)
                    {
                        float t = elapsedTime / durationTransition;
                        VRObjectTransform.localPosition = Vector3.Lerp(startPos, endPos, t * t * (3f - 2f * t));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    } 
                    VRObjectTransform.localPosition = endPos;
                    if (ball != null)
                    {
                        ball.labyReady = true;
                    }
                break;

            case 1:
                if (clouds)
                {
                    durationTransition = 2.0f;
                    elapsedTime = 0.0f;
                    startPos = VRObjectTransform.localPosition;
                    endPos = new Vector3(0f, 600f, 200f);
                    TheVRObject.Instance.FaceController.Enable();

                    Quaternion startRotation = VRObjectTransform.rotation;
                    Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

                    while (elapsedTime < durationTransition)
                    {   
                        float t = elapsedTime / durationTransition;
                        rightCloud.transform.Translate(Vector3.right * 2);
                        leftCloud.transform.Translate(Vector3.left * 2);
                        VRObjectTransform.localPosition = Vector3.Lerp(startPos, endPos, t * t * (3f - 2f * t));
                        VRObjectTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t * t * (3f - 2f * t));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    TheVRObject.Instance.StartLaserPhase();
                    laser.isActive = true;
                    VRObjectTransform.localPosition = endPos;
                    rightCloud.SetActive(false);
                    leftCloud.SetActive(false);
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                Debug.Log("Erreur de fin de phase");
                break;
        }
        
    }

    public void CheckLamps()
    {
        foreach(Lamp lamp in lamps)
        {
            if(!lamp.IsLit)
            {
                return;
            }
        }
        AchievePhase(laserPhase);

    }

}
