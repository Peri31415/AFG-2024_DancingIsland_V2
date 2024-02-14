using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDialogue : MonoBehaviour
{
    //Camera related
    public Component main_Camera;
    public Transform initialPos, targetPos;

    private Transform target;
    private string interactingWith = "";

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
        if (other.gameObject.tag == "Player" && dialogueInteractions.Length != 0)
        {
            if ( && numInteractionsPerStage == 0)
            interactingWith = gameObject.name;
            TriggerDialogue(DialogueAccordingToNumInteractionsAndGameStage());

            //else
            //if (other.gameObject.tag == "Player")
            //{
            //    toggleConversation.SetActive(true);

            //    if (Input.GetKey(KeyCode.E))
            //{
            //    interactingWith = gameObject.name;
            //    TriggerDialogue(DialogueAccordingToNumInteractionsAndGameStage());

            //    toggleConversation.SetActive(false);
            //}



            //switch (MyGameManager.Instance.gameStage)
            //{
            //    case "Start":
            //        TriggerDialogue (DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "First Trial":
            //        TriggerDialogue (DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "First Trial Completed":
            //        TriggerDialogue (DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "Second Trial":
            //        TriggerDialogue (DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "Second Trial Completed":
            //        TriggerDialogue (DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "Third Trial":
            //        TriggerDialogue(DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "Third Trial Completed":
            //        TriggerDialogue(DialogueAccordingToNumInteractionsAndGameStage());
            //        break;
            //    case "Game Finished":
            //        TriggerDialogue(DialogueAccordingToNumInteractionsAndGameStage());
            //        break;            
            //}
        }
    }

    public void DialogueFinished()
    {
        interactingWith = "";
        numInteractionsPerStage += 1;

        //if ( && numInteractionsPerStage == 0)

        switch (MyGameManager.Instance.gameStage)
        {
            case "Start":
                MyGameManager.Instance.gameStage = "First Trial";
                break;
            case "First Trial Completed":
                MyGameManager.Instance.gameStage = "Second Trial";
                break;
            case "Second Trial Completed":
                MyGameManager.Instance.gameStage = "Third Trial";
                break;
            case "Third Trial Completed":
                MyGameManager.Instance.gameStage = "Game Finished";
                break;            
        }
    }

    public Dialogue DialogueAccordingToNumInteractionsAndGameStage()
    {
        foreach (Dialogue d in dialogueInteractions)
        {
            if (d.gameStage == MyGameManager.Instance.gameStage)          
                return d;            
        }

        return null;
    }
}
