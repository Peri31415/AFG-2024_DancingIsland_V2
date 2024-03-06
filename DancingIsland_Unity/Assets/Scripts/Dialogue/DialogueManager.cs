using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText, dialogueText;

    public Animator animator;

    private Queue<string> sentences, dialogueKeys; //Look up FIFO collections
    private ObjectDialogue objectDialogue;

    bool dialogueOn = false;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueKeys = new Queue<string>();
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
            sentences.Enqueue(sentence);

        foreach (string key in dialogue.dialogueKeys)
            dialogueKeys.Enqueue(key);

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
        
        gameObject.GetComponent<DialogueAudioTrigger>().PlayDialogue(dialogueKeys.Dequeue());

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

        objectDialogue.DialogueFinished();
        objectDialogue = null;

        dialogueOn = false;

        MouseAndMovementUnlock();
    }

    private void MouseAndMovementLock()
    {
        PlayerManager.instance.mouseLook.enabled = false;
        PlayerManager.instance.playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;        
    }

    private void MouseAndMovementUnlock()
    {
        PlayerManager.instance.mouseLook.enabled = true;
        PlayerManager.instance.playerMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;        
    }
}
