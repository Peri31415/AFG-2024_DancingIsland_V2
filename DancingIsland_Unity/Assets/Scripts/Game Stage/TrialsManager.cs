using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class TrialsManager : MonoBehaviour
{
    #region Singleton

    public static TrialsManager instance;

    private void Awake() 
    {
        instance = this;
    }

    #endregion

    public Timer trialsTimer;
    public TextMeshProUGUI trialsInfo;
    public GameObject crossHair;
    public Transform trialOneTargets, trialThreeTargets;
    public Transform parkourStartingPos, playerMainIslandPos;    
}
