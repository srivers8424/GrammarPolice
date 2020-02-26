using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantDontDestroyGroup : MonoBehaviour
{
    public static PersistantDontDestroyGroup instance; //Container object for all managers
                                                        //that need to be persistent between scenes

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }
}



////GameManager State Machine 
//public enum GameStateInterval { PARKSCENE, CUTSCENE_INTRO, COMBAT_WAVE_0, CUTSCENE_MID, COMBAT_BOSSFIGHT, CUTSCENE_END }
////Park-->CutScene-->Enemies-->Cutscene-->Boss-->Cutscene
//public int currentState; //current state value 0-5
//public Action GameplayTransition;
//public GameplayTransition OnStateTransition;

//public void OnStateTransition()
//{
//    //Fade to black

//    //Cue next state in order based on current state
//}