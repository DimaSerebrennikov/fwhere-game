using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioS : MonoBehaviour
{
    //=========================================================== Редактор
    public AudioClip ac1;
    public AudioClip ac2;
    public AudioClip ac3;
    //=========================================================== Редактор
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayRandomSound()
    {
        int a = 0;
        a = Random.Range(0, 3);
        if (a == 0)
        {
            audioSource.clip = ac1;
        }
        else if (a == 1)
        {
            audioSource.clip = ac2;
        }
        else
        {
            audioSource.clip = ac3;
        }
        audioSource.Play();
    }
}
