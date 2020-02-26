/*  @author Sonya Rivers-Medina
 *  @date 4/2/2019
 *  @Last Edited 2/12/2020
 *  @purpose Manages the Audio Sting sub-system of the game to trigger
 *           sting music to play (fading in) when an intense moment occurs
 *           then fades back into normal background music when the moment ends
 */
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;

namespace SonyaScripts
{
    public class StingMusicControl : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot outOfSting;
        [SerializeField] private AudioMixerSnapshot inSting;
        [SerializeField] private float m_TransitionIn; 
        [SerializeField] private float m_TransitionOut;
        [SerializeField] private float bpm = 128;
        [SerializeField] private AudioClip[] stings;
        private AudioSource stingSource;
        private float m_QuarterNote;
       
        #region Scene Loading Events

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            //Subscribe to the event listener for the Audio Manager so that
            //stings are triggered in each scene
            AudioManager.instance.OnEnterSting += EnterStingMusicEvent;
            AudioManager.instance.OnExitSting += ExitStingMusicEvent;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            //Unsubscribe from the event listener for the Audio Manager so that
            //stings are no longer triggered in each scene
            AudioManager.instance.OnEnterSting -= EnterStingMusicEvent;
            AudioManager.instance.OnExitSting -= ExitStingMusicEvent;
        }

        /// <summary>
        /// Assigns audio source and other related scene values when the scene loads
        /// and this object becomes enabled in the scene hierarchy
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SetStingSource();
            SetTransitionFadeBMP();
        }

        /// <summary>
        ///  Measures the tempo to optimize fade (BPM == Beats Per Minute).
        ///  In/out transition times are in milliseconds to fade music transitions
        ///  from quiet to action snapshots in the Master Audio Mixer Asset
        /// </summary>
        private void SetTransitionFadeBMP()
        {
            m_QuarterNote = 60 / bpm; //A quarter of the beats per minute in milliseconds
            m_TransitionIn = m_QuarterNote * 0.8f; //fade in quickly over the length of one quarter note
            m_TransitionOut = m_QuarterNote * 16; // 8 bars to fade out slowly
        }

        /// <summary>
        /// Assigns the default sting source if there isn't one when the scene loads. For 3D 
        /// trigger zone enter/exit objects the sting should be set in the inspector by default
        /// </summary>
        private void SetStingSource()
        {
            if (stingSource == null && AudioManager.instance != null)
            {
                stingSource = AudioManager.instance.stingMusic;
            }
        }
        #endregion


        #region Trigger-Based Enter/Exit Stings from 3D World Interaction Events

        /// <summary>
        /// Transitions INTO high intensity sting music when colliding with in-game trigger.
        /// (Ideally) To be attached to a 3D object or predefined 3D space.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                inSting.TransitionTo(m_TransitionIn);
                PlaySting();
                //AudioManager.instance.SetMainMusicVolume(-80f);
            }
        }

        /// <summary>
        /// Transitions OUT of high intensity sting music when colliding with in-game trigger.
        /// (Ideally) To be attached to a 3D object or predefined 3D space.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                outOfSting.TransitionTo(m_TransitionOut);
               // AudioManager.instance.SetMainMusicVolume(-80f);
            }
        }
        #endregion

        #region Trigger-Based Enter/Exit Stings from Scripting Interaction Events

        /// <summary>
        /// Plays a random index from array of sounds to assign to stings audio source
        /// </summary>
        private void PlaySting()
        {
            int randClip = UnityEngine.Random.Range(0, stings.Length);
            stingSource.clip = stings[randClip];
            stingSource.Play();
        }

       

        /// <summary>
        /// Transitions into high intensity sting audio when EnterSting event is triggered
        /// </summary>
        public void EnterStingMusicEvent()
        {
            
            inSting.TransitionTo(m_TransitionIn);
            PlaySting();
           // AudioManager.instance.SetMainMusicVolume(-80f);
        }

        /// <summary>
        /// Transitions out of sting audio when ExitSting event is triggered
        /// </summary>
        public void ExitStingMusicEvent()
        {
            outOfSting.TransitionTo(m_TransitionOut);
            // AudioManager.instance.SetMainMusicVolume(-20f);
            StartCoroutine(EndClipTransition());
        }

        /// <summary>
        /// Stops a sting's audio source from playing after a predefined 
        /// transition time (m_TransitionOut) delay.
        /// </summary>
        /// <returns></returns>
        private IEnumerator EndClipTransition()
        {
            yield return new WaitForSeconds(m_TransitionOut);
            stingSource.Stop();
        }

        #endregion

       



    }
}
