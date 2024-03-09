using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
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

    public Dialogue DialogueAccordingToGameStage()
    {
        foreach (Dialogue d in dialogueInteractions)
        {
            if (d.gameStage == MyGameManager.instance.currentGameStage && d.numberOfInteraction == numInteractionsPerStage)          
                return d;
        }

        return null;
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().SartDialogue(dialogue, this);
    }

    void TriggerDialogueAfterSound()
    {
        interactingWith = gameObject.name;
        TriggerDialogue(DialogueAccordingToGameStage());        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 0);

        if (other.gameObject.tag == "Player" && dialogueInteractions.Length != 0 && DialogueAccordingToGameStage() != null)
        {
            if (this.gameObject == GameObject.FindWithTag("Entity") && numInteractionsPerStage == 0)
            {
                PlayerManager.instance.MouseAndMovementLock();

                //Audio
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsDialogueActive", 1);

                //Wait 2 seconds before triggering Dialgue anim and audio only if it is the currentGameStage is equeal to First Trial Completed
                if (MyGameManager.instance.currentGameStage == "Start" || MyGameManager.instance.currentGameStage == "Second Trial Completed")
                    Invoke (nameof (TriggerDialogueAfterSound), 6);
                else
                    Invoke (nameof (TriggerDialogueAfterSound), 3);
            }            
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Player" && dialogueInteractions.Length != 0 && DialogueAccordingToGameStage() != null)
        {
            if (this.gameObject != GameObject.FindWithTag("Entity"))
            {
                toggleConversation.SetActive(true); 

                if (Input.GetKey(KeyCode.E))
                {
                    interactingWith = gameObject.name;
                    toggleConversation.SetActive(false); 
                    
                    TriggerDialogue(DialogueAccordingToGameStage());               
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
                    MyGameManager.instance.SetSecondtTrial();
                    numInteractionsPerStage = 0;
                    break;
                case "Second Trial Completed":
                    MyGameManager.instance.currentGameStage = "Third Trial";
                    MyGameManager.instance.SetThirdTrial();
                    numInteractionsPerStage = 0;
                    break;
                case "Third Trial Completed":
                    MyGameManager.instance.currentGameStage = "Game Finished";
                    numInteractionsPerStage = 0;

                    TrialsManager.instance.youWinCanvas.SetActive(true);
                    PlayerManager.instance.MouseAndMovementLock();

                    //Audio
                    //StopAll
                    AudioManager.instance.playOneShot("event:/VO/Pavip_NPC/Congratulations_Ending");
                    break;            
            }
        }        
    }
}
