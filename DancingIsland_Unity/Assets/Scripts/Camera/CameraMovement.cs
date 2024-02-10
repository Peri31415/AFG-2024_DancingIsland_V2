using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Component main_Camera;
    public Transform initialPos, targetPos;
    public DialogueTrigger dialogueTrigger;

    private Transform target;
    private string dialogue = "";

    public void Update()
    {
        float duration = Time.deltaTime * 3;
        Vector3 pos = main_Camera.transform.position;
        Quaternion rot = main_Camera.transform.rotation;

        if (dialogue == gameObject.name)                    
            target = targetPos;        

        else       
            target = initialPos;                    

        Debug.Log("Anything happening?");
        main_Camera.transform.position = Vector3.Lerp(pos, target.position, duration);
        main_Camera.transform.rotation = Quaternion.Lerp(rot, target.rotation, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogue = gameObject.name;
            dialogueTrigger.TriggerDialogue();
        }
    }

    public void DialogueFinished()
    {
        target = initialPos;
        
    }
}
