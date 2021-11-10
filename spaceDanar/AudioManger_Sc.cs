using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger_Sc : MonoBehaviour
{

    public static AudioManger_Sc Instance;
    public List<AudioSource> AudioSources;
    public List<AudioClip> Clips;


    void Awake()
    {
        Instance = this;
    }

    public void PlayClip(string clipName)
    {

        var clip = Clips.Find(x => x.name == clipName);
        if (clip != null)
        {
            bool foundEmptyAudioSource = false;
            foreach (var audioSource in AudioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    foundEmptyAudioSource = true;
                    break;
                }
            }
            if (!foundEmptyAudioSource)
            {
                var audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.playOnAwake = false;
                audioSource.Play();
                AudioSources.Add(audioSource);
            }
        }
    }
}
