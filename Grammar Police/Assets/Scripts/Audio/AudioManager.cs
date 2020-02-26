/*  @author Sonya Rivers-Medina
 *  @date 4/2/2019
 *  @Last Edited 2/12/2020
 *  @purpose Manages the audio system of the game
 */

using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SonyaScripts
{
    public class AudioManager : MonoBehaviour
    {
        #region Private Serialized Class Member Variables
        [SerializeField] private AudioMixer mixer = null;
        [SerializeField] private Slider masterSlider = null;
        [SerializeField] private Slider mainMusicSlider = null;
        [SerializeField] private Slider sfxSlider = null;
        [SerializeField] private Slider ambienceSlider = null;
        [SerializeField] private Slider stingSlider = null;
        #endregion

        #region Public Get/Set Set Member Properties
        //Each will be used by other scripts to get current values
        //but the public access to Set() modifications occur with 
        //their respective SetVolume() functions
       
        #endregion

        #region Public Class Member Variables
        public static AudioManager instance;
        //Audio Source Game Object Components 
        public AudioSource mainMusic;
        public AudioSource stingMusic;
        public AudioSource sfxSound;
        public AudioSource ambientSound;
        //Adjustable Volume Levels
        public float masterVolume;
        public float mainMusicVolume;
        public float ambienceVolume;
        public float stingVolume;
        public float sfxVolume;
        //Event Actions to be subscribed to by Event Listeners
        public event Action OnEnterSting;
        public event Action OnExitSting;

        #endregion

        /// <summary>
        /// Initializes singleton instance
        /// </summary>
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            //TODO: ONLY SET DEFAULTS IF PLAYER SETTINGS ARE NON-EXISTENT!
            SetDefaults();
        }

        #region Get/Set Default Configurations
        /// <summary>
        /// Gets the current Volume Levels from the Audio Mixer Asset as float values
        /// and assigns them to the Audio Manager
        /// </summary>
        public void GetVolumeLevels()
        {
            mixer.GetFloat("MasterVolume", out masterVolume);
            mixer.GetFloat("MainMusicVolume", out mainMusicVolume);
            mixer.GetFloat("StingMusicVolume", out stingVolume);
            mixer.GetFloat("AmbienceVolume", out ambienceVolume);
            mixer.GetFloat("SFXVolume", out sfxVolume);
        }

        /// <summary>
        /// Resets to default settings. Should only be called if user settings
        /// are undefined OR user has manaually reset their settings to default
        /// by selecting a reset from the Audio Settings UI menu
        /// </summary>
        public void SetDefaults()
        {
            mixer.SetFloat("MasterVolume", 0f);
            mixer.SetFloat("MainMusicVolume", -20f);
            mixer.SetFloat("StingMusicVolume", -80f);
            mixer.SetFloat("AmbienceVolume", -10f);
            mixer.SetFloat("SFXVolume", -30f);

            if (masterSlider != null)
            {
                masterSlider.value = 0f;
            }

            if (mainMusicSlider != null)
            {
                mainMusicSlider.value = -20f;
            }

            if (ambienceSlider != null)
            {
                ambienceSlider.value = -10f;
            }

            if (stingSlider != null)
            {
                stingSlider.value = -80f;
            }

            if (sfxSlider != null)
            {
                sfxSlider.value = -30f;
            }
        }

        #endregion

        #region Set Volume by UI Menu Slider Input Events

        /// <summary>
        /// Sets the master mixer volume to a UI slider's float value
        /// </summary>
        public void SetMasterVolumeBySlider()
        {
            if (masterSlider != null)
            {
                mixer.SetFloat("MasterVolume", masterSlider.value);
                masterVolume = masterSlider.value + 0.8f;
            }
            else
            {
                Debug.Log("AUDIO MANAGER ERROR: NULL SLIDER FOR MASTER VOLUME");
            }
        }

        /// <summary>
        /// Sets the main background music's volume to a slider's float value
        /// </summary>
        public void SetMainMusicVolumeBySlider()
        {
            if (mainMusicSlider != null)
            {
                mixer.SetFloat("MainMusicVolume", mainMusicSlider.value);
                mainMusicVolume = mainMusicSlider.value;
            }
            else
            {
                Debug.Log("AUDIO MANAGER ERROR: NULL SLIDER FOR MAIN MUSIC VOLUME");
            }
        }

        /// <summary>
        /// Sets the sting music's volume to a slider's float value
        /// </summary>
        public void SetStingMusicVolumeBySlider()
        {
            if (stingSlider != null)
            {
                mixer.SetFloat("StingMusicVolume", stingSlider.value);
                stingVolume = stingSlider.value;
            }
            else
            {
                Debug.Log("AUDIO MANAGER ERROR: NULL SLIDER FOR STING MUSIC VOLUME");
            }
        }

        /// <summary>
        /// Sets the ambience music's volume to a slider's float value
        /// </summary>
        public void SetAmbientVolumeBySlider()
        {
            if (ambienceSlider != null)
            {
                mixer.SetFloat("AmbienceVolume", ambienceSlider.value);
                ambienceVolume = ambienceSlider.value;
            }
            else
            {
                Debug.Log("AUDIO MANAGER ERROR: NULL SLIDER FOR AMBIENCE VOLUME");
            }
        }

        /// <summary>
        /// Sets the SFX volume to a slider's float value
        /// </summary>
        public void SetSFXVolumeBySlider()
        {
            if (sfxSlider != null)
            {
                mixer.SetFloat("SFXVolume", sfxSlider.value);
                sfxVolume = sfxSlider.value;
            }
            else
            {
                Debug.Log("AUDIO MANAGER ERROR: NULL SLIDER FOR SFX VOLUME");
            }
        }

        #endregion


        #region Set Volume By Scripting Event

        /// <summary>
        /// Sets the main background music's volume to a float value
        /// </summary>
        public void SetMainMusicVolume(float volume)
        {
            mixer.SetFloat("MainMusicVolume", volume);
            mainMusicVolume = volume;
        }

        /// <summary>
        /// Sets the sting music's volume to a float value
        /// </summary>
        public void SetStingMusicVolume(float volume)
        {
            mixer.SetFloat("StingMusicVolume", volume);
            stingVolume = volume;
        }

        /// <summary>
        /// Sets the ambience music's volume to a float value
        /// </summary>
        public void SetAmbientVolume(float volume)
        {
            mixer.SetFloat("AmbienceVolume", volume);
            ambienceVolume = volume;
        }

        /// <summary>
        /// Sets the sting music's volume to a float value
        /// </summary>
        public void SetSFXMusicVolume(float volume)
        {
            mixer.SetFloat("SFXVolume", volume);
            sfxVolume = volume;
        }

        #endregion


        #region Event-Based Audio Triggers
        public void OnStingEnter()
        {
            //Validate the event Action is not null
            if (OnEnterSting != null)
            {
                OnEnterSting();
            }
        }

        public void OnStingExit()
        {
            //Validate the event Action is not null
            if (OnEnterSting != null)
            {
                OnExitSting();
            }
        }

        /// <summary>
        /// Instantly plays a one-shot instance of a sting music clip
        /// </summary>
        /// <param name="sting"></param>
        public void PlaySting_OneShot(AudioClip sting)
        {
            stingMusic.PlayOneShot(sting);
        }

        /// <summary>
        /// Plays a continuous loop of a sting music clip at
        /// a specific volume if there isn't already a sting
        /// loop currently playing
        /// </summary>
        /// <param name="sting"></param>
        public void PlaySting_EnterLoop(AudioClip sting, float volume)
        {
            //Validate that there ISN'T already sting music playing
            if (!stingMusic.isPlaying)
            { 
                //Currently handled by StingMusicControl script!

                //TODO: Ease in/out of volume gradually!
                //stingMusic.clip = sting;
                //SetStingMusicVolume(volume);
                // stingMusic.Play();
            }
        }

        /// <summary>
        /// Exits a continuous loop of a sting music clip if there is one playing
        /// a sting loop currently playing
        /// </summary>
        /// <param name="sting"></param>
        public void PlaySting_ExitLoop()
        {
            //Validate that there IS already sting music playing
            if (stingMusic.isPlaying)
            {
                //Currently handled by StingMusicControl script!
            }
        }

        public void PlaySFX(AudioClip SFX)
        {
            sfxSound.PlayOneShot(SFX);
        }
        #endregion

        #region Audio Configuration Settings

        /// <summary>
        /// Saves Audio Configuration Settings to be saved in the database's Player Settings
        /// </summary>
        public void SaveAudioSettings()
        {
            float masterVol, mainMusicVol, stingVol, ambienceVol, sfxVol;
            mixer.GetFloat("MasterVolume", out masterVol);
            mixer.GetFloat("MainMusicVolume", out mainMusicVol);
            mixer.GetFloat("StingMusicVolume", out stingVol);
            mixer.GetFloat("AmbienceVolume", out ambienceVol);
            mixer.GetFloat("SFXVolume", out sfxVol);

            //TODO: REWRITE PLAYER MENU SETTINGS CONFIG SCRIPT!
            //PlayerMenuSettingsConfig_UI.playerSettings.MasterVolume = Convert.ToInt32(masterVol);
            //PlayerMenuSettingsConfig_UI.playerSettings.MainMusicVolume = Convert.ToInt32(mainMusicVol);
            //PlayerMenuSettingsConfig_UI.playerSettings.StingMusicVolume = Convert.ToInt32(stingVol);
            //PlayerMenuSettingsConfig_UI.playerSettings.AmbienceVolume = Convert.ToInt32(ambienceVol);
            //PlayerMenuSettingsConfig_UI.playerSettings.SFXVolume = Convert.ToInt32(sfxVol);
        }

        /// <summary>
        /// Loads Audio Configuration Settings from the database's Player Settings
        /// </summary>
        public void LoadAudioSettings()
        {
            //mixer.SetFloat("MasterVolume", PlayerMenuSettingsConfig_UI.playerSettings.MasterVolume);
            //mixer.SetFloat("MainMusicVolume", PlayerMenuSettingsConfig_UI.playerSettings.MainMusicVolume);
            //mixer.SetFloat("StingMusicVolume", PlayerMenuSettingsConfig_UI.playerSettings.StingMusicVolume);
            //mixer.SetFloat("AmbienceVolume", PlayerMenuSettingsConfig_UI.playerSettings.AmbienceVolume);
            //mixer.SetFloat("SFXVolume", PlayerMenuSettingsConfig_UI.playerSettings.SFXVolume);

            //masterSlider.value = PlayerMenuSettingsConfig_UI.playerSettings.MasterVolume;
            //mainMusicSlider.value = PlayerMenuSettingsConfig_UI.playerSettings.MainMusicVolume;
            //stingSlider.value = PlayerMenuSettingsConfig_UI.playerSettings.StingMusicVolume;
            //ambienceSlider.value = PlayerMenuSettingsConfig_UI.playerSettings.AmbienceVolume;
            //sfxSlider.value = PlayerMenuSettingsConfig_UI.playerSettings.SFXVolume;
        }

        #endregion

    }
}

