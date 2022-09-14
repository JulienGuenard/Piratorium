using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource[] audioSourceArray;

    void Start()
    {
        audioSourceArray = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioSourceArray.Length; i++)
        {
            audioSourceArray[i].Play();
        }
    }

    void Update()
    {
        for (int i = 0; i < audioSourceArray.Length; i++)
        {
            if (audioSourceArray[i].volume < 0.9f) return;
        }

        GameManager.instance.Win();
    }
}
