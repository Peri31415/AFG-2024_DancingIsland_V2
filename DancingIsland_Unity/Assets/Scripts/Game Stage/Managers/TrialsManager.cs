using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Burst.CompilerServices;

public class TrialsManager : MonoBehaviour
{
    #region Singleton

    public static TrialsManager instance;

    private void Awake() 
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    #endregion

    public Timer trialsTimer;
    public TextMeshProUGUI trialsInfo;
    public GameObject crossHair;
    public Transform trialOneTargets, trialThreeTargets;
    public Transform parkourStartingPos, playerMainIslandPos;    

    [HideInInspector] public int trialOneTargetNumber, trialThreeEnemyNumber;

    private void Start() 
    {
        trialOneTargetNumber = trialOneTargets.childCount;
        trialThreeEnemyNumber =  trialThreeTargets.childCount;
    }
}
