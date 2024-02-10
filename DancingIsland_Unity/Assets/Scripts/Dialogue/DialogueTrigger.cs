using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().SartDialogue(dialogue, this.transform.gameObject, cameraMovement);
    }
}
