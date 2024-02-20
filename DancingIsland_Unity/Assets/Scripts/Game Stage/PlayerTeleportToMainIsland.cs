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

                GameObject.Find ("TrialsTimer").GetComponent<Timer>().enabled = false;

                GameObject.FindWithTag ("Player").transform.position = GameObject.Find ("PlayerBackToMainIsland").transform.position;
                GameObject.FindWithTag ("Player").transform.rotation = GameObject.Find ("PlayerBackToMainIsland").transform.rotation;

                MyGameManager.Instance.currentGameStage = "Second Trial Completed";
            }    
        }
    }
}