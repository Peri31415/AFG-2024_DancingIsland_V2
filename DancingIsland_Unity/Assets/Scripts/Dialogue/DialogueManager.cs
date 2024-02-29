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
    private ObjectDialogue objectDialogue;

    bool dialogueOn = false;

    private int i = 0;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void SartDialogue(Dialogue dialogue, ObjectDialogue objectDial)
    {
        dialogueOn = true;
        
        objectDialogue = objectDial;

        MouseAndMovementLock();

        //dialogueTrigger.SetActive(false);

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
        if (sentences.Count == 0 && dialogueOn == true)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));        
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    
    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        i += 1;

        Debug.Log ("Dialogue Finished Called " + i);
        objectDialogue.DialogueFinished();
        objectDialogue = null;

        dialogueOn = false;

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
