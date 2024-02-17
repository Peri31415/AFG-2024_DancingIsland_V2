using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance;

    public string currentGameStage = "Start";

    private string[] gameStages= 
    {"Start", "First Trial", "First Trial Completed", "Second Trial", "Second Trial Completed", "Third Trial", "Third Trial Completed"};

    private GameObject[] firstTrialObjects, secondTrialObjects, thirdTrialObjects, allTrialObjects;

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

        allTrialObjects = firstTrialObjects.Concat(secondTrialObjects.Concat(thirdTrialObjects)).ToArray();

        SetStart();
    }

    private void SetStart()
    {
        foreach (GameObject obj in allTrialObjects)
            obj.gameObject.SetActive(false);        
    }

    public void SetFirstTrial()
    {
        foreach (GameObject obj in firstTrialObjects)
            obj.gameObject.SetActive(true);            
    }

    public void SetSecondtTrial()
    {
        foreach (GameObject obj in secondTrialObjects)
            obj.gameObject.SetActive(true);
    }

    public void SetThirdTrial()
    {
        foreach (GameObject obj in thirdTrialObjects)
            obj.gameObject.SetActive(true);
    }
}