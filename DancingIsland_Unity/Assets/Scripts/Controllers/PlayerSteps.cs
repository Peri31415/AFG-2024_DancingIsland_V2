using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    //public FMOD.Studio.EventInstance playerStepsCodigo;
    FMODUnity.StudioEventEmitter playerSteps;

    private float material = 0f;
    
    private void Start() 
    {
        playerSteps = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    public void MaterialChecking()
    {
        RaycastHit hit;
    
        Physics.Raycast(transform.position, Vector3.down, out hit, 10f);

        Debug.Log ("Material: " + hit.collider.tag);

        if (hit.collider)
        {
            switch (hit.collider.tag)
            {
                    case "Material_Sand":
                        material = 0f;
                        break;
                    case "Material_Dirt":
                        material = 1f;
                        break;
                    case "Material_Grass":
                        material = 2f;
                        break;
                    case "Material_Rock":
                        material = 3f;
                        break; 
                    case "Material_Wood":
                        material = 4f;
                        break;

                    default:
                        material = 0f;
                        break;
            }

            //playerStepsCodigo.setParameterByName("MaterialCheck", material);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MaterialCheck", material);
        }
    }

    public void StartEvent()
    {
        if (!playerSteps.IsPlaying())
            playerSteps.Play();
        // //Cannot initialise event within start function because it will play that instance and once it is done destroy it
        //playerStepsCodigo = FMODUnity.RuntimeManager.CreateInstance("event:/Foley/FPP/Steps");        
    }

    public void StopEvent()
    {
        playerSteps.Stop();
    }
}
