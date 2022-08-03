using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    AudioSource audioS;
    MeshRenderer[] meshRendererArray;

    [Range(0,1)] public float volumeUpSpeed;
    [Range(0, 1)] public float volumeDownSpeed;

    public Material matLightOn;
    public Material matLightOff;

    void Awake()
    {
        audioS = GetComponent<AudioSource>();
        meshRendererArray = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        audioS.volume -= volumeDownSpeed * 0.01f;

        for (int i = 0; i < meshRendererArray.Length; i++)
        {
            if (audioS.volume > (float)i / meshRendererArray.Length)
            {
                

                meshRendererArray[i].material = matLightOn;
            }else
            {
                meshRendererArray[i].material = matLightOff;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioS.volume += volumeUpSpeed;
    }
}
