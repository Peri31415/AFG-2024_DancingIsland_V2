using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    #endregion

    [HideInInspector] public PlayerSteps playerSteps; //Script holding audio behaviour for player steps
    GameObject amb_parkourAmbEmmitter;

    FMOD.Studio.EventInstance mx_oneShot_drumHit, mx_oneShot_drumRoll, mx_oneShot_throatSinging; //One shot music events
    FMOD.Studio.EventInstance mx_main; //Main music

    FMOD.Studio.EventInstance amb_seaside, amb_wind, amb_islandCentre;

    FMOD.Studio.EventInstance f_Timer;    

    void Start()
    {
        playerSteps = PlayerManager.instance.getPlayerStepsEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //Updating FMOD global parameter "GameStage"
        switch(MyGameManager.instance.currentGameStage)
        {
            case "Start":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 0);
            break;

            case "First Trial":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 1);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Bira_TargetCount", MyGameManager.instance.targetCount);
            break;

            case "First Trial Completed":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 2);
            break;

            case "Second Trial":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 3);
            //Trigger Parkour Snapshot
            break;

            case "Second Trial Completed":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 4);
            //Trigger End_Parkour Snapshot
            break;

            case "Third Trial":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 5);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SugiBan_EnemyCount", MyGameManager.instance.targetCount);
            break;

            case "Third Trial Completed":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 6);
            break;

            case "PlayerDeath":
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStage", 7);
            break;
        }
    }

    public void playStart()
    {
        mx_oneShot_throatSinging = FMODUnity.RuntimeManager.CreateInstance("event:/MX/OneShots/TuvanThroatSinging");
        mx_oneShot_throatSinging.start();
        mx_oneShot_throatSinging.release();

        amb_seaside = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/Seaside");
        amb_seaside.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        amb_seaside.start();
        amb_seaside.release();

        amb_wind = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/Wind");
        amb_wind.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        amb_wind.start();
        amb_wind.release();

        amb_islandCentre = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/IslandCentre");
        amb_islandCentre.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        amb_islandCentre.start();
        amb_islandCentre.release();
    }

    public void playDeath()
    {
        mx_oneShot_drumRoll = FMODUnity.RuntimeManager.CreateInstance("event:/MX/OneShots/DrumRoll");
        mx_oneShot_drumRoll.start();
        mx_oneShot_drumRoll.release();
    }

    public void playMainMusic()
    {
        mx_main = FMODUnity.RuntimeManager.CreateInstance("event:/MX/MainMusic");
        mx_main.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        mx_main.start();
        mx_main.release();
    }

    public void playDeathSentence()
    {
        //Gen a rand number between 0 and 1 and make a condiconal statement to play one of the two dynamically
        
        gameObject.GetComponent<DialogueAudioTrigger>().PlayDialogue("Entity/Defeat/vo_entity_defeat_1");
        //gameObject.GetComponent<DialogueAudioTrigger>().PlayDialogue("Entity/Defeat/vo_entity_defeat_1");
    }

    // public void dialogueActive() //Not working God knows why
    // {
    //     mx_oneShot_drumHit = FMODUnity.RuntimeManager.CreateInstance("event:/MX/OneShots/DrumHit");
    //     mx_oneShot_drumHit.start();
    //     mx_oneShot_drumHit.release();
        
    //     playerSteps.stopSteps();
    //     mx_main.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    // }

    // public void dialogueNotActive() //Not working God knows why
    // {
    //     playMainMusic();
    //     playerSteps.playSteps();
    // }

    public void setGameStageChangedMusicParam()
    {
        if(MyGameManager.instance.currentGameStage == "First Trial Completed" || MyGameManager.instance.currentGameStage == "Second Trial Completed" || MyGameManager.instance.currentGameStage == "Third Trial Completed")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStageChanged", 1);
        }
    }

    public void anchorAmbienceToParkour()
    {
        amb_seaside.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(amb_parkourAmbEmmitter.gameObject));
        amb_wind.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(amb_parkourAmbEmmitter.gameObject));
    } 

    public void anchorAmbienceToMainIsland()
    {
        amb_seaside.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        amb_wind.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
    } 

    public void setTimerSound (int timerType)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TimerSound", timerType);
    }

    public void startStopTimer()
    {
         if (PlaybackState(f_Timer) != FMOD.Studio.PLAYBACK_STATE.PLAYING)
         {
            f_Timer = FMODUnity.RuntimeManager.CreateInstance("event:/FX/UI/Timer");
            f_Timer.start();
            f_Timer.release();
         }

         else
            f_Timer.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);        
    }

    public FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance) 
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        instance.getPlaybackState(out playbackState);
        return playbackState;
    }

    public void playOneShot(string eventPath)
    {
        //Declaration
        FMOD.Studio.EventInstance instance;

        //Initialisation
        instance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        instance.start();
        instance.release();
    }
}
