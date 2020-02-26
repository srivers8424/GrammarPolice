/*  @author Sonya Rivers-Medina
 *  @date 2/25/2020
 *  @purpose Manages the transitions between scene audio systems
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


namespace SonyaScripts
{
    public class SceneAudioTransition : MonoBehaviour
    {
        [SerializeField] private List<SceneAudioDisplay> sceneAudios;
        private SceneAudioDisplay currentSceneAudio;

        private void Awake()
        {
            //Disable all the active scene audio components
            //so that they're only enabled by event calls
            foreach(SceneAudioDisplay s in sceneAudios)
            {
                if(s.gameObject.activeInHierarchy)
                {
                    s.gameObject.SetActive(false);
                }
            }

            //Reactivate the first scene audio object
            sceneAudios[0].gameObject.SetActive(true);
            currentSceneAudio = sceneAudios[0];
        }

        /// <summary>
        /// Manually assigns the scene audio by a given string for the
        /// current scene's name
        /// </summary>
        /// <param name="sceneViewName"></param>
        public void SetSceneAudio(string sceneViewName)
        {
            //If it's setting the scene audio to the current configuration
            //exit this method
            if(sceneViewName == currentSceneAudio.name)
            {
                Debug.Log("Attempted to set scene audio to current config");
                return;
            }

            currentSceneAudio.gameObject.SetActive(false);

            switch(sceneViewName)
            {
                case "SceneAudio_IntroScene":
                    sceneAudios[0].gameObject.SetActive(true);
                    currentSceneAudio = sceneAudios[0];
                    break;
                case "SceneAudio_MiniLesson":
                    sceneAudios[1].gameObject.SetActive(true);
                    currentSceneAudio = sceneAudios[1];
                    break;
                case "SceneAudio_PoliceChase":
                    sceneAudios[2].gameObject.SetActive(true);
                    currentSceneAudio = sceneAudios[2];
                    break;

            }
        }
    }
}


