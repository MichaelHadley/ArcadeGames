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
    public bool Inited { get; private set; }

    [Header ("Player Info")]
    //public GameObject player;
    public Transform playerSpawn;
    public GameObject playerSprite;
    public int curDir;

    [Header("Traffic")]
    public GameObject Truck;
    public GameObject car;
    private GameObject truckSprite;
    private GameObject carSprite;
    
    [Header("River Items")]
    public GameObject Log;
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

    [Header("Instantiated Objects")]
    public Transform roadObjects;
    public Transform riverObjects;

    [Header ("Audio")]
    public AudioClip splashClip;
    public AudioClip jumpClip;

    [Header ("Lane variables")]
    public float laneGap = 2f;
    public int playerLane;
    public int maxPlayerLane;

    private float[] laneProgress;
    private float[] laneWait;
    private int numOfLanes;
    private laneType[] laneDefinition;
    private bool isMoving;

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
    
    private SpriteRenderer spriteRenderer;

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
                carSprite.transform.SetParent(roadObjects);
                Traffic.Add(carSprite);
            }
            else
            {
                truckSprite = Instantiate(Truck, objEnd.position, Quaternion.identity);
                truckSprite.transform.SetParent(roadObjects);
                Traffic.Add(truckSprite);
            }
        }

        //River Objects spawn
        for (int i = 0; i < 10; i++)
        {
            logSprite = Instantiate(Log, objEnd.position, Quaternion.identity);
            logSprite.transform.SetParent(riverObjects);
            Logs.Add(logSprite);

        }
        for (int i = 0; i < 10; i++)
        {
            lilypadSprite = Instantiate(LilyPad, objEnd.position, Quaternion.identity);
            lilypadSprite.transform.SetParent(riverObjects);
            LilyPads.Add(lilypadSprite);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        //playerSprite = Instantiate(player, playerSpawn.position, Quaternion.identity);
        playerSprite.transform.position = playerSpawn.position;

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
                    //left to right
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
                //move across screen
                if (laneDefinition[i] == laneType.RoadLane)
                {
                    Traffic[curTrafficItem].transform.position = thisObjStart + new Vector3(0, laneGap * i, 0);
                    if (i % 2 == 0)
                    {
                        Traffic[curTrafficItem].transform.rotation = new Quaternion(0, 180, 0, 0);

                    }
                    else
                    {
                        Traffic[curTrafficItem].transform.rotation = new Quaternion(0, 0, 0, 0);

                    }
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

                    LilyPads[curLilyPadItem].GetComponent<SpriteManager>().UpdateSprite();

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
            playerSprite.transform.DOMoveY(playerSprite.transform.position.y + laneGap, 0.2f);
            playerSprite.transform.Rotate(0, 0, (curDir - 0) * 90);
            curDir = 0;
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpClip, 2f);
            playerLane++;
            StartCoroutine("LandingCheck");
        }
    }
    public void MoveBackwards()
    {
        if (!isMoving)
        {
            if (playerLane > 0)
            {
                isMoving = true;
                playerSprite.transform.DOMoveY(playerSprite.transform.position.y - laneGap, 0.2f);
                playerSprite.transform.Rotate(0, 0, (curDir - 2) * 90);
                curDir = 2;
                gameObject.GetComponent<AudioSource>().PlayOneShot(jumpClip, 2f);
                playerLane--;
                StartCoroutine("LandingCheck");
            }
        }
    }
    public void MoveRight()
    {
        if (!isMoving)
        {
            //this gets the player position reletive to the screen view with the screen x value going from 0 to 1, stops the player going off screen
            Vector3 playerRelPos = Camera.main.WorldToViewportPoint(playerSprite.transform.GetChild(1).transform.position + new Vector3(laneGap, 0, 0));
            if (playerRelPos.x < 1f)
            {
                isMoving = true;
                playerSprite.transform.DOMoveX(playerSprite.transform.position.x + laneGap, 0.2f);
                playerSprite.transform.Rotate(0, 0, (curDir - 1) * 90);
                curDir = 1;
                gameObject.GetComponent<AudioSource>().PlayOneShot(jumpClip, 2f);
                StartCoroutine("LandingCheck");
            }
        }
    }
    public void MoveLeft()
    {
        if (!isMoving)
        {
            //this gets the player position reletive to the screen view with the screen x value going from 0 to 1, stops the player going off screen
            Vector3 playerRelPos = Camera.main.WorldToViewportPoint(playerSprite.transform.GetChild(0).transform.position - new Vector3(laneGap, 0, 0));
            if (playerRelPos.x > 0)
            {
                isMoving = true;
                playerSprite.transform.DOMoveX(playerSprite.transform.position.x - laneGap, 0.2f);
                playerSprite.transform.Rotate(0, 0, (curDir - 3) * 90);
                curDir = 3;
                gameObject.GetComponent<AudioSource>().PlayOneShot(jumpClip, 2f);
                StartCoroutine("LandingCheck");
            }
        }
    }

    IEnumerator LandingCheck()
    {
        yield return new WaitForSeconds(0.2f); //time to wait between movements
        //playerLane++;
        isMoving = false;
        landedOn = null;
        playerSprite.transform.parent = null;
                
        //only for water lanes do we need to check if landed on object
        if(laneDefinition[playerLane] == laneType.LilyPadLane)
        {
            //Loop through all lilypads to see if in right lane and if frog is on lilypad
            //Add lilypad gameobject to LandedOn variable
            foreach(GameObject curlilypad in LilyPads)
            {
                if(Mathf.Round(playerSprite.transform.position.y) == Mathf.Round(curlilypad.transform.position.y))
                {
                    if (playerSprite.transform.position.x > curlilypad.transform.GetChild(0).transform.position.x
                        && playerSprite.transform.position.x < curlilypad.transform.GetChild(1).transform.position.x)
                    {
                        landedOn = curlilypad;
                    }
                }
            }
        }
        else if (laneDefinition[playerLane] == laneType.LogLane)
        {
            foreach (GameObject curLog in Logs)
            {
                if (Mathf.Round(playerSprite.transform.position.y) == Mathf.Round(curLog.transform.position.y))
                {
                    if (playerSprite.transform.position.x > curLog.transform.GetChild(0).transform.position.x
                        && playerSprite.transform.position.x < curLog.transform.GetChild(1).transform.position.x)
                    {
                        landedOn = curLog;
                    }
                }
            }
        }

        if (laneDefinition[playerLane] == laneType.LogLane || laneDefinition[playerLane] == laneType.LilyPadLane)
        {
            if (landedOn)
            {
                //update to say landed on something
                playerSprite.transform.parent = landedOn.transform;
                if(playerLane > maxPlayerLane)
                {
                    maxPlayerLane = playerLane;
                    FroggerManager.Instance.AwardPoints("laneProgress");
                }
            }
            else
            {
                //play splash sound
                gameObject.GetComponent<AudioSource>().PlayOneShot(splashClip);
                //player is dead
                FroggerManager.Instance.Death();
            }
        }
        else
        {
            //if landed in a lane and survive add points
            if (playerLane > maxPlayerLane)
            {
                maxPlayerLane = playerLane;
                //call add  points for lane progress
                FroggerManager.Instance.AwardPoints("laneProgress");
            }
        }
    }
}
