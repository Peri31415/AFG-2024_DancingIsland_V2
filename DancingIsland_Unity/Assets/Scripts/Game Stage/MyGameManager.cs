using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance;

    public string currentGameStage = "Start";

    [Serialize] public GameObject player;

    // [SerializeField] private string[] gameStages= 
    // {"Start", "First Trial", "First Trial Completed", "Second Trial", "Second Trial Completed", "Third Trial", "Third Trial Completed"};

    private GameObject[] firstTrialObjects, secondTrialObjects, thirdTrialObjects, allTrialsObjects, TotalObjects;

    [HideInInspector] public int targetCount = 0;

    private int firstTrialTargetNumber, thirdtTrialTargetNumber = 0;

    //public int playerHealth = 100;
    //public int gameTimer = 5;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        firstTrialObjects = GameObject.FindGameObjectsWithTag("First Trial");
        secondTrialObjects = GameObject.FindGameObjectsWithTag("Second Trial");
        thirdTrialObjects = GameObject.FindGameObjectsWithTag("Third Trial");
        allTrialsObjects = GameObject.FindGameObjectsWithTag("All Trials");

        TotalObjects = allTrialsObjects.Concat(firstTrialObjects.Concat(secondTrialObjects.Concat(thirdTrialObjects))).ToArray();

        firstTrialTargetNumber = GameObject.Find ("FirstTrialTargets").transform.transform.childCount;
        //thirdtTrialTargetNumber = GameObject.Find ("ThirdTrialTargets").transform.transform.childCount;

        SetStart();
    }

    private void Update() 
    {
        if (currentGameStage == "First Trial" && targetCount == firstTrialTargetNumber)
        {
            currentGameStage = "First Trial Completed";

            GameObject.Find ("TrialsTimer").GetComponent<Timer>().enabled = false;

            foreach (GameObject obj in firstTrialObjects)
                obj.gameObject.SetActive(false);
        }

        if (currentGameStage == "Third Trial" && targetCount == thirdtTrialTargetNumber)
        {
            currentGameStage = "Third Trial Completed";

            foreach (GameObject obj in thirdTrialObjects)
                obj.gameObject.SetActive(false);
        }
    }

    private void SetStart()
    {
        foreach (GameObject obj in TotalObjects)
            obj.gameObject.SetActive(false);                      
    }

    public void SetFirstTrial()
    {
        foreach (GameObject obj in firstTrialObjects)
            obj.gameObject.SetActive(true);

        foreach (GameObject obj in allTrialsObjects)
            obj.gameObject.SetActive(true);
    }

    public void SetSecondtTrial()
    {
        foreach (GameObject obj in secondTrialObjects)
            obj.gameObject.SetActive(true);

        GameObject.Find ("TrialsTimer").GetComponent<Timer>().enabled = true;
        
        player.transform.position = GameObject.Find ("PlayerParkourStartingPos").transform.position;
        player.transform.rotation = GameObject.Find ("PlayerParkourStartingPos").transform.rotation;
    }

    public void SetThirdTrial()
    {
        foreach (GameObject obj in thirdTrialObjects)
            obj.gameObject.SetActive(true);
    }
}