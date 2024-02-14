using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance;

    public string gameStage = "Start";

    // private string[] gameStages = 
    // {"Start", "First Trial", "First Trial Completed", "Second Trial", "Second Trial Completed", "Third Trial", "Third Trial Completed"};

    //public int playerHealth = 100;
    //public int gameTimer = 5;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    //  private void ProgressGameStage()
    //  {
    //     gameStage = gameStages.next
    //  }
}