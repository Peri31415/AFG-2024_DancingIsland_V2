using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText, dialogueText;
    public Animator animator;
    public MouseLook mouseLook;
    public PlayerMovement playerMovement;

    private Queue<string> sentences; //Look up FIFO collections
    private CameraMovement cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void SartDialogue(Dialogue dialogue, GameObject dialogueTrigger, CameraMovement camera)
    {
        cameraMovement = camera;

        MouseAndMovementLock();

        dialogueTrigger.SetActive(false);

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        cameraMovement.DialogueFinished();

        MouseAndMovementUnlock();
    }

    private void MouseAndMovementLock()
    {
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;        
    }

    private void MouseAndMovementUnlock()
    {
        mouseLook.enabled = true;
        playerMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;        
    }
}
