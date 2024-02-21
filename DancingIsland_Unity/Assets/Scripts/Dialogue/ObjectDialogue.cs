using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDialogue : MonoBehaviour
{
    //Camera related
    public Component main_Camera;
    public Transform initialPos, targetPos;

    private Transform target;
    private string interactingWith = "";

    //TextMeshPro "ToggleConversation" component
    public GameObject toggleConversation;

    //Dialogue
    public Dialogue[] dialogueInteractions;

    //Interactions with this particular NPC
    protected int numInteractionsPerStage = 0;    

    public void Update() //This will be used for the camera transform
    {
        float duration = Time.deltaTime * 3;
        Vector3 pos = main_Camera.transform.position;
        Quaternion rot = main_Camera.transform.rotation;

        if (interactingWith == gameObject.name)
            target = targetPos;

        else
            target = initialPos;

        main_Camera.transform.position = Vector3.Lerp(pos, target.position, duration);
        main_Camera.transform.rotation = Quaternion.Lerp(rot, target.rotation, duration);
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().SartDialogue(dialogue, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && dialogueInteractions.Length != 0 && DialogueAccordingToGameStage() != null)
        {
            if (this.gameObject == GameObject.FindWithTag("Entity") && numInteractionsPerStage == 0)
            {
                interactingWith = gameObject.name;
                TriggerDialogue(DialogueAccordingToGameStage());
            }            

            else
            {
                toggleConversation.SetActive(true);

                if (Input.GetKey(KeyCode.F))
                {
                interactingWith = gameObject.name;
                TriggerDialogue(DialogueAccordingToGameStage());

                toggleConversation.SetActive(false);                    
                }
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (toggleConversation.activeSelf == true)        
            toggleConversation.SetActive(false);        
    }

    public void DialogueFinished()
    {
        interactingWith = "";
        //numInteractionsPerStage += 1;

        if (this.gameObject == GameObject.FindWithTag("Entity"))
        {            
            switch (MyGameManager.instance.currentGameStage)
            {
                case "Start":
                    MyGameManager.instance.currentGameStage = "First Trial";
                    MyGameManager.instance.SetFirstTrial();
                    numInteractionsPerStage = 0;
                    break;
                case "First Trial Completed":
                    MyGameManager.instance.currentGameStage = "Second Trial";
                    //MyGameManager.Instance.setFirstTrial;
                    MyGameManager.instance.SetSecondtTrial();
                    numInteractionsPerStage = 0;
                    break;
                case "Second Trial Completed":
                    MyGameManager.instance.currentGameStage = "Third Trial";
                    //MyGameManager.Instance.setFirstTrial;
                    MyGameManager.instance.SetThirdTrial();
                    numInteractionsPerStage = 0;
                    break;
                case "Third Trial Completed":
                    MyGameManager.instance.currentGameStage = "Game Finished";
                    //MyGameManager.Instance.setFirstTrial;
                    numInteractionsPerStage = 0;
                    break;            
            }
        }        
    }

    public Dialogue DialogueAccordingToGameStage()
    {
        foreach (Dialogue d in dialogueInteractions)
        {
            if (d.gameStage == MyGameManager.instance.currentGameStage && d.numberOfInteraction == numInteractionsPerStage)          
                return d;
        }

        return null;
    }
}
