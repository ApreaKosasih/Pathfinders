using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    public Animator transition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioWithDelay());
    }

    IEnumerator PlayAudioWithDelay()
    {

        yield return new WaitForSeconds(1.0f); // Delay 1 detik

        audioSource.clip = audioClip;
        audioSource.Play();

    }
}
