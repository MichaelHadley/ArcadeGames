using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Frogger : MonoBehaviour
{
    private static Frogger _instance;
    public static Frogger Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Frogger>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }

    public bool Inited
    {
        get; private set;
    }

    [Header ("Player Infor")]
    public GameObject player;
    public Transform playerSpawn;
    private GameObject playerSprite;

    [Header("Traffic")]
    public GameObject Truck;
    public GameObject car;
    private GameObject truckSprite;
    private GameObject carSprite;
    
    
    [Header("River Items")]
    public GameObject log;
    public GameObject LilyPad;
    private GameObject lilypadSprite;
    private GameObject logSprite;
    private GameObject landedOn;

    [Header("Object start and end positions")]
    public Transform objStart;
    public Transform objEnd;
    public Transform objStart2;
    public Transform objEnd2;

    [Header("Lists")]
    private List<GameObject> Traffic = new List<GameObject>();
    private List<GameObject> Logs = new List<GameObject>();
    private List<GameObject> LilyPads = new List<GameObject>();
    private int curTrafficItem = 0;
    private int curLogItem = 0;
    private int curLilyPadItem = 0;
  
    private float[] laneProgress;
    private float[] laneWait;
    private int numOfLanes;
    private laneType[] laneDefinition;
    public float laneGap = 2f;

    enum laneType
    {
        StartLane,
        RoadLane,
        SafetyLane,
        LogLane,
        LilyPadLane,
        EndLane,
        MAX
    }

    private int playerLane;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        numOfLanes = 11;
        playerLane = 0;
        laneProgress = new float[numOfLanes];
        laneWait = new float[numOfLanes];
        laneDefinition = new laneType[numOfLanes];

        laneDefinition[0] = laneType.StartLane;
        for (int i = 1; i < 5; i++)
        {
            laneDefinition[i] = laneType.RoadLane;
        }
        laneDefinition[5] = laneType.SafetyLane;
        laneDefinition[6] = laneType.LogLane;
        laneDefinition[7] = laneType.LogLane;
        laneDefinition[8] = laneType.LilyPadLane;
        laneDefinition[9] = laneType.LilyPadLane;
        laneDefinition[10] = laneType.EndLane;

        //Traffic Objects Spawn
        for (int i = 0; i < 20; i++)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                carSprite = Instantiate(car, objEnd.position, Quaternion.identity);
                Traffic.Add(carSprite);
            }
            else
            {
                truckSprite = Instantiate(Truck, objEnd.position, Quaternion.identity);
                Traffic.Add(truckSprite);
            }
        }

        //River Objects spawn
        for (int i = 0; i < 20; i++)
        {
            logSprite = Instantiate(log, objEnd.position, Quaternion.identity);
            Logs.Add(logSprite);

        }
        for (int i = 0; i < 20; i++)
        {
            lilypadSprite = Instantiate(LilyPad, objEnd.position, Quaternion.identity);
            LilyPads.Add(lilypadSprite);
        }
        
        playerSprite = Instantiate(player, playerSpawn.position, Quaternion.identity);

        for (int i = 0; i < numOfLanes; i++)
        {
            laneProgress[i] = 1f;
            laneWait[i] = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ObjectMovement();
    }

    public void ObjectMovement()
    {
        Vector3 thisObjStart;
        Vector3 thisObjEnd;

        for (int i = 1; i < numOfLanes - 1; i++)
        {
            laneProgress[i] += Time.deltaTime / laneWait[i];
            if (laneProgress[i] >= 1)
            {
                if (i % 2 == 0)
                {
                    thisObjStart = objStart.position;
                    thisObjEnd = objEnd.position;
                }
                else
                {
                    //right to left
                    thisObjStart = objStart2.position;
                    thisObjEnd = objEnd2.position;
                }
                laneProgress[i] = 0f;
                //left to right 
                if (laneDefinition[i] == laneType.RoadLane)
                {
                    Traffic[curTrafficItem].transform.position = thisObjStart + new Vector3(0, laneGap * i, 0);
                    Traffic[curTrafficItem].transform.DOMoveX(thisObjEnd.x, 10f).SetEase(Ease.Linear);
                    curTrafficItem++;
                    if (curTrafficItem == Traffic.Count)
                    {
                        curTrafficItem = 0;
                    }
                    laneWait[i] = Random.Range(2f, 4f);
                }
                else if (laneDefinition[i] == laneType.LogLane)
                {
                    Logs[curLogItem].transform.position = thisObjStart + new Vector3(0, laneGap * i, 0);
                    Logs[curLogItem].transform.DOMoveX(thisObjEnd.x, 10f).SetEase(Ease.Linear);
                    curLogItem++;
                    if (curLogItem == Logs.Count)
                    {
                        curLogItem = 0;
                    }
                    laneWait[i] = Random.Range(2f, 4f);
                }
                else if (laneDefinition[i] == laneType.LilyPadLane)
                {
                    LilyPads[curLilyPadItem].transform.position = thisObjStart + new Vector3(0, laneGap * i, 0);
                    LilyPads[curLilyPadItem].transform.DOMoveX(thisObjEnd.x, 10f).SetEase(Ease.Linear);
                    curLilyPadItem++;
                    if (curLilyPadItem == LilyPads.Count)
                    {
                        curLilyPadItem = 0;
                    }
                    laneWait[i] = Random.Range(2f, 4f);
                }
            }
        }
    }
    
    public void MoveForwards()
    {
        if (!isMoving)
        {
            isMoving = true;
            playerSprite.transform.DOMoveY(playerSprite.transform.position.y + laneGap, 0.5f);
            playerLane++;
            StartCoroutine("LandingCheck");
        }
    }
    public void MoveBackwards()
    {
        if (!isMoving)
        {
            isMoving = true;
            playerSprite.transform.DOMoveY(playerSprite.transform.position.y - laneGap, 0.5f);
            playerLane--;
            StartCoroutine("LandingCheck");
        }
    }
    public void MoveRight()
    {
        if (!isMoving)
        {
            isMoving = true;
            playerSprite.transform.DOMoveX(playerSprite.transform.position.x + laneGap, 0.5f);
            StartCoroutine("WaitForMovement");
        }
    }
    public void MoveLeft()
    {
        if (!isMoving)
        {
            isMoving = true;
            playerSprite.transform.DOMoveX(playerSprite.transform.position.x - laneGap, 0.5f);
            StartCoroutine("WaitForMovement");
        }
    }

    IEnumerator WaitForMovement()
    {
        yield return new WaitForSeconds(0.5f); //time to wait between movements
        //playerLane++;
        isMoving = false;
    }
        IEnumerator LandingCheck()
    {
        yield return new WaitForSeconds(0.5f); //time to wait between movements
        //playerLane++;
        isMoving = false;
        if(laneDefinition[playerLane] == laneType.EndLane)
        {
            //TODO
            //end game
            DOTween.KillAll();
            FindObjectOfType<GameManager>().EndGame();
        }
        //only for water lanes do we need to check if landed on object
        if(laneDefinition[playerLane] == laneType.LilyPadLane)
        {
            //Loop through all lilypads to see if in right lane and if frog is on lilypad
            //Add lilypad gameobject to LandedOn variable
            foreach(GameObject curlilypad in LilyPads)
            {
                if(player.transform.position.y == curlilypad.transform.position.y)
                {
                    if (player.transform.position.x > curlilypad.transform.GetChild(0).transform.position.x
                        && player.transform.position.x < curlilypad.transform.GetChild(1).transform.position.x)
                    {
                        landedOn = curlilypad;
                    }
                }
            }
        }
        else if (laneDefinition[playerLane] == laneType.LogLane)
        {

        }
    }
}
