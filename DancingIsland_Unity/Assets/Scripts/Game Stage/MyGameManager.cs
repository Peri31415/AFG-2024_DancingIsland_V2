using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance;

    public string currentGameStage = "Start";

    private GameObject[] firstTrialObjects, secondTrialObjects, thirdTrialObjects, allTrialsObjects, TotalObjects;

    [HideInInspector] public int targetCount = 0;

    private int trialOneTargetNumber, trialThreeEnemyNumber = 0;

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

        trialOneTargetNumber = TrialsManager.instance.trialOneTargets.childCount;
        trialThreeEnemyNumber = TrialsManager.instance.trialThreeTargets.childCount;

        SetStart();
    }

    private void Update() 
    {
        if (currentGameStage == "First Trial" && targetCount == trialOneTargetNumber)
        {
            TrialComplete();
            currentGameStage = "First Trial Completed";

            foreach (GameObject obj in firstTrialObjects)
                obj.gameObject.SetActive(false);
        }

        if (currentGameStage == "Third Trial" && targetCount == trialThreeEnemyNumber)
        {
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
        
        PlayerManager.instance.player.transform.position = TrialsManager.instance.parkourStartingPos.position;
        PlayerManager.instance.player.transform.rotation = TrialsManager.instance.playerMainIslandPos.rotation;
    }

    public void SetThirdTrial()
    {
        foreach (GameObject obj in thirdTrialObjects)
            obj.gameObject.SetActive(true);
    }

    private void TrialComplete()
    {
        TrialsManager.instance.trialsTimer.enabled = false;
        TrialsManager.instance.trialsInfo.text = "Talk to the Entity";

        TrialsManager.instance.crossHair.SetActive(false);
    }
}