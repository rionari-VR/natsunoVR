using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSrc : MonoBehaviour
{
    //SoundClips
    public AudioClip StartSE;
    public AudioClip ResultSE;
    public AudioClip TenSecLeft;
    public AudioClip ThirtySecLeft;
    public AudioClip EndSE;
    public AudioClip Hit1;
    public AudioClip Hit2;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Get Audio Source
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   //play sounds
        // audioSource.PlayOneShot(Start);
        // audioSource.PlayOneShot(Result);
        // audioSource.PlayOneShot(TenSecLeft);
        // audioSource.PlayOneShot(ThirtySecLeft);
        // audioSource.PlayOneShot(ThirtySecLeft);
        // audioSource.PlayOneShot(End);
        // audioSource.PlayOneShot(Hit1);
        // audioSource.PlayOneShot(Hit2);
    }
}
