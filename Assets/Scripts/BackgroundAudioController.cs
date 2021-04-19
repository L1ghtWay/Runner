using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] backgroundSounds;
    private AudioSource backgroundAudiosource;

    void Start()
    {
        backgroundAudiosource = GetComponent<AudioSource>();

        StartCoroutine(BackgroundSoundPlay());    
    }

    private IEnumerator BackgroundSoundPlay()
    {
        while (true)
        {
            for (int i = 0; i < backgroundSounds.Length; i++)
            {
                backgroundAudiosource.clip = backgroundSounds[i];
                backgroundAudiosource.Play();
                yield return new WaitForSeconds(backgroundSounds[i].length);
            }
        }

             
    }      
    
}
