using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeleportToMainIsland : MonoBehaviour
{
    public GameObject teleportText;

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
             teleportText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                teleportText.SetActive(false);

                PlayerManager.instance.player.transform.position = TrialsManager.instance.playerMainIslandPos.position;
                PlayerManager.instance.player.transform.rotation = TrialsManager.instance.playerMainIslandPos.rotation;

                if (MyGameManager.instance.currentGameStage == "Second Trial")
                {
                    TrialsManager.instance.trialsTimer.enabled = false;
                    MyGameManager.instance.TrialComplete();
                    MyGameManager.instance.currentGameStage = "Second Trial Completed";

                    //Audio
                    AudioManager.instance.anchorAmbienceToMainIsland();
                }        

                //Audio
                AudioManager.instance.playOneShot("event:/FX/Collectables/Teleport/Teleport_fx");     
            }    
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Player")
            teleportText.SetActive(false);
    }
}