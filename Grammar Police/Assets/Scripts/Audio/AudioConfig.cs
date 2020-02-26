/*  @author Sonya Rivers-Medina
 *  @date 2/26/2019
 *  @purpose Manipulates the control systems for audio components
 */
 
using UnityEngine;

namespace SonyaScripts
{
    public class AudioConfig : MonoBehaviour
    {
        [SerializeField] private AudioSource mainMenuSoundByte = null;
        
        //Resets to default settings
        public void SetDefaults()
        {
            SetBG(1.00f);
            SetSFX(0.80f);
            SetAtm(0.60f);
            SetAudioType("Stereo");
        }

        //Sets the background music volume
        public void SetBG(float bgVolume)
        {
            mainMenuSoundByte.volume = bgVolume;
        }

        //Sets the sound effects volume
        public void SetSFX(float sfxVolume)
        {
            AudioSource[] audios = GameObject.FindObjectsOfType<AudioSource>();  //<---FIX ME!!! Should not be gameobject.find! Should be mixer-> expose attenuation level
            foreach (AudioSource source in audios)
            {
                source.volume = sfxVolume;
            }
        }

        //Sets the atmospheric sound volume
        public void SetAtm(float atmVolume)
        {
            AudioSource[] audios = GameObject.FindObjectsOfType<AudioSource>();  //<-----------FIX ME!!! Should not be gameobject.find!
            foreach (AudioSource source in audios)
            {
                source.volume = atmVolume;
            }
        }

        //Sets the type of speaker mode output
        public void SetAudioType(string SpeakerMode)
        {
            switch (SpeakerMode)
            {
                case "Mono":
                    AudioSettings.speakerMode = AudioSpeakerMode.Mono;
                    break;
                case "Stereo":
                    AudioSettings.speakerMode = AudioSpeakerMode.Stereo;
                    break;
                case "Surround":
                    AudioSettings.speakerMode = AudioSpeakerMode.Surround;
                    break;
                case "Surround 5.1":
                    AudioSettings.speakerMode = AudioSpeakerMode.Mode5point1;
                    break;
                case "Surround 7.1":
                    AudioSettings.speakerMode = AudioSpeakerMode.Mode7point1;
                    break;
            }
        }
    }

}
