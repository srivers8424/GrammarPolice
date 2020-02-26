/*  @author Sonya Rivers-Medina
 *  @date 4/9/2019
 *  @purpose  Display that allows each scene to have its own music
 *            then sends it to the preconfiugred Audio Controller
 */

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace SonyaScripts
{
    public class SceneAudioDisplay : MonoBehaviour
    {
        public GameSceneAudio sceneAudio;
        public static SceneAudioDisplay instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            SetSceneAudio();
        }

        public void SetSceneAudio()
        {
            //Make sure the volume level is correct for each scene
            if (sceneAudio.mainSceneMusicVolume != AudioManager.instance.mainMusicVolume)
            {
                AudioManager.instance.SetMainMusicVolume(sceneAudio.mainSceneMusicVolume);
            }
            if (sceneAudio.ambientSoundVolume != AudioManager.instance.ambienceVolume)
            {
                AudioManager.instance.SetAmbientVolume(sceneAudio.ambientSoundVolume);
            }

            //If there isn't only one track on the main music list
            //AND it's not supposed to play the intro track on start, 
            //then play a random song in the list
            if (sceneAudio.ambientSound != null && sceneAudio.playInitTrack)
            {
               // PlayAmbienceStingOneShot_InitSceneMusic();
            }

            AudioManager.instance.mainMusic.clip = sceneAudio.mainSceneMusic[GetMainSceneMusicRange()];

            if (sceneAudio.auxSceneMusic[0] != null)
            {
                AudioManager.instance.stingMusic.clip = sceneAudio.auxSceneMusic[0];
            }
            if (sceneAudio.ambientSound[0] != null)
            {
                AudioManager.instance.ambientSound.clip = sceneAudio.ambientSound[0];
            }

           



            AudioManager.instance.mainMusic.Play();
            //AudioManager.instance.auxMusic.Play(); //No need to play the stings on Start()
            AudioManager.instance.ambientSound.Play();
        }

        public IEnumerator SetSceneAudioOnDelay()
        {
            yield return new WaitForSeconds(1.0f);
            SetSceneAudio();
        }
        public int GetMainSceneMusicRange()
        {
            return (int)Random.Range(0.0f, (float)sceneAudio.mainSceneMusic.Count);
        }

        public int GetMainSceneMusicRange_ExcludeInit()
        {
            return (int)Random.Range(1, (float)sceneAudio.mainSceneMusic.Count);
        }

        /// <summary>
        /// Plays a sting one-shot for special effect
        /// </summary>
        public void PlayStingOneShot(int stingIndex)
        {
            AudioManager.instance.stingMusic.PlayOneShot(sceneAudio.auxSceneMusic[stingIndex]);
        }

        /// <summary>
        /// Plays a sting one-shot for special effect
        /// </summary>
        public void PlayAmbienceStingOneShot_InitSceneMusic()
        {
            AudioManager.instance.ambientSound.PlayOneShot(sceneAudio.ambientSound[0]);
        }

        /// <summary>
        /// Plays a sting one-shot for special effect
        /// </summary>
        public void PlayStingOneShot(AudioClip sting)
        {
            AudioManager.instance.stingMusic.PlayOneShot(sting);
        }

        /// <summary>
        /// Adjusts the scene's main music volume when the scene loads in
        /// </summary>
        private void SetMainSceneMusicVolume()
        {

        }

        /// <summary>
        /// Adjusts the scene's ambience volume when the scene loads in
        /// </summary>
        private void SetAmbienceVolume()
        {

        }

        /// <summary>
        /// Resets the audio source clip after another clip has finished playing
        /// </summary>
        /// <param name="source"></param>
        /// <param name="clip"></param>
        private IEnumerator ResetAudioClip(AudioSource source, AudioClip clip)
        {
            //Wait until the current clip is finished
            yield return new WaitUntil(() => !source.isPlaying);
            source.clip = clip;

        }


    }
}

