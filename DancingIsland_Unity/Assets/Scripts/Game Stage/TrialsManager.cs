using UnityEngine;
using TMPro;

public class TrialsManager : MonoBehaviour
{
    #region Singleton

    public static TrialsManager instance;

    private void Awake() 
    {
        instance = this;
    }

    #endregion

    public TextMeshProUGUI trialsInfo;
    public Timer trialsTimer;
    public GameObject trialOneTargets, trialThreeTargets;

    public Transform parkourStartingPos, playerMainIslandPos;    
}
