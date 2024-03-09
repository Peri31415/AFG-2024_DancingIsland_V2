using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spaceship : MonoBehaviour
{
    public GameObject getOutOfSpaceshipText;
    public Transform outOfVehiclePos;

    private void Awake() 
    {
        GetComponent<MeshCollider>().enabled = false;
    }

    private void Start() 
    {
        PlayerManager.instance.playerCharacter.SetActive(false);
        PlayerManager.instance.playerMovement.enabled = false;
        PlayerManager.instance.mouseLook.setMinAndMaxClamp (-70, 70);

        if (gameObject.GetComponent<BoxCollider>().enabled == false)
            Invoke (nameof (enableCollider), 20.0f);
    }

    private void enableCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;  
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            getOutOfSpaceshipText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                getOutOfSpaceshipText.SetActive(false);
                
                PlayerManager.instance.player.transform.position = outOfVehiclePos.position;
                PlayerManager.instance.player.transform.rotation = outOfVehiclePos.rotation;

                PlayerManager.instance.playerCharacter.SetActive(true);
                PlayerManager.instance.playerMovement.enabled = true;
                PlayerManager.instance.mouseLook.setMinAndMaxClamp (-90, 40);

                gameObject.GetComponent<MeshCollider>().enabled = true;

                //Audio
                AudioManager.instance.playMainMusic();
            }
        }
    }
}
