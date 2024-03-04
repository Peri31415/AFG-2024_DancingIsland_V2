using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    #region Singleton

    public static MyGameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    #endregion
    
    public string currentGameStage = "Start";

    public Transform startPlayerPosition;

    private GameObject[] firstTrialObjects, secondTrialObjects, thirdTrialObjects, allTrialsObjects, TotalObjects;

    [HideInInspector] public int targetCount = 0;

    private void Start() 
    {
        firstTrialObjects = GameObject.FindGameObjectsWithTag("First Trial");
        secondTrialObjects = GameObject.FindGameObjectsWithTag("Second Trial");
        thirdTrialObjects = GameObject.FindGameObjectsWithTag("Third Trial");
        allTrialsObjects = GameObject.FindGameObjectsWithTag("All Trials");

        TotalObjects = allTrialsObjects.Concat(firstTrialObjects.Concat(secondTrialObjects.Concat(thirdTrialObjects))).ToArray();

        SetStart();
    }

    private void Update() 
    {
        if (currentGameStage == "First Trial" && targetCount == TrialsManager.instance.trialOneTargetNumber)
        {
            targetCount = 0;

            TrialComplete();
            currentGameStage = "First Trial Completed";

            foreach (GameObject obj in firstTrialObjects)
                obj.gameObject.SetActive(false);
        }

        if (currentGameStage == "Third Trial" && targetCount == TrialsManager.instance.trialThreeEnemyNumber)
        {
            targetCount = 0;
            
            TrialComplete();
            currentGameStage = "Third Trial Completed";

            foreach (GameObject obj in thirdTrialObjects)
                obj.gameObject.SetActive(false);
        }
    }

    private void SetStart()
    {
        foreach (GameObject obj in TotalObjects)
            obj.gameObject.SetActive(false);         

        PlayerManager.instance.player.transform.position = startPlayerPosition.position;
        //PlayerManager.instance.player.transform.rotation = startPlayerPosition.rotation;
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

        TrialsManager.instance.trialsTimer.enabled = true;
        TrialsManager.instance.trialsInfo.text = "Climb To The Top";
        
        PlayerManager.instance.player.transform.position = TrialsManager.instance.parkourStartingPos.position;
        PlayerManager.instance.player.transform.rotation = TrialsManager.instance.parkourStartingPos.rotation;
    }

    public void SetThirdTrial()
    {
        foreach (GameObject obj in thirdTrialObjects)
            obj.gameObject.SetActive(true);

        TrialsManager.instance.trialsTimer.enabled = true;
        TrialsManager.instance.trialsInfo.text = "Enemies down: ";
    }

    public void TrialComplete()
    {
        TrialsManager.instance.trialsTimer.enabled = false;
        TrialsManager.instance.trialsInfo.text = "Talk to the Entity";

        TrialsManager.instance.crossHair.SetActive(false);
    }
}