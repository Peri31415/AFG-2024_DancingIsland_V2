using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    FMOD.Studio.EventInstance playerSteps, playerJump;

    private float material = 0f;

    public void MaterialChecking()
    {
        RaycastHit hit;
    
        Physics.Raycast(transform.position, Vector3.down, out hit, 10f);

        //Debug.Log ("Material: " + hit.collider.tag);

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

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MaterialCheck", material);
        }
    }

    public void playSteps()
    {
        if (AudioManager.instance.PlaybackState(playerSteps) != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            playerSteps = FMODUnity.RuntimeManager.CreateInstance("event:/Foley/FPP/Steps");
            playerSteps.start();
            playerSteps.release();
        }
    }

    public void stopSteps()
    {
        playerSteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void setRunningTrue()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsRunning", 1);
    }

    public void setRunningFalse()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsRunning", 0);
    }

    public void jump()
    {
        playerJump = FMODUnity.RuntimeManager.CreateInstance("event:/Foley/FPP/Jump");
        playerJump.start();
        playerJump.release();
    }
}
