using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public ObjectDialogue cameraMovement;
    //public Dialogue dialogue;

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().SartDialogue(dialogue, cameraMovement);
    }
}
