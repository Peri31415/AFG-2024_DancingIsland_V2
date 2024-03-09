using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake() 
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;        
    }

    #endregion

    public GameObject player, playerCharacter;
    public MouseLook mouseLook;
    public PlayerMovement playerMovement;

    private void Start() 
    {
        
    }

    public void MouseAndMovementLock()
    {
        mouseLook.enabled = false;
        instance.playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;        
    }

    public void MouseAndMovementUnlock()
    {
        mouseLook.enabled = true;
        instance.playerMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;        
    }

    public PlayerSteps getPlayerStepsEvent()
    {
        return player.GetComponent<PlayerSteps>();
    }
}
