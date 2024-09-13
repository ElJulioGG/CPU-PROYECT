using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest2 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
         audioSource = GetComponent<AudioSource>();
         audioSource.clip = clip;
         
    }
    public void PlaySound()
    {
        audioSource.Play();
    }
}
