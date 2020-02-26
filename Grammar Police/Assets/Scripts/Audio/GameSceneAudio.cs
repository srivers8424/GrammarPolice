/*  @author Sonya Rivers-Medina
 *  @date 4/9/2019
 *  @purpose Allows each scene to have its own music
 *           then sends it to the preconfiugred Audio Controller
 */

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameSceneAudio : ScriptableObject
{
    public List<AudioClip> mainSceneMusic;
    public List<AudioClip> auxSceneMusic;
    public List<AudioClip> ambientSound;
    public float mainSceneMusicVolume;
    public float ambientSoundVolume;
    public bool playInitTrack;
    
}


