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

    //Collider or Button
    //public DialogueTrigger dialogueTrigger;

    //Dialogue
    public Dialogue[] dialogueInteractions;


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
            interactingWith = gameObject.name;

            switch (MyGameManager.Instance.gameStage)
            {
                case 0:
                    TriggerDialogue(dialogueInteractions[0]);
                    break;
                case 1:
                    TriggerDialogue(dialogueInteractions[1]);
                    break;
                case 2:
                    TriggerDialogue(dialogueInteractions[2]);
                    break;
                case 3:
                    TriggerDialogue(dialogueInteractions[3]);
                    break;
                case 4:
                    TriggerDialogue(dialogueInteractions[4]);
                    break;
            }
        }
    }

    public void DialogueFinished()
    {
        interactingWith = "";    
    }
}
