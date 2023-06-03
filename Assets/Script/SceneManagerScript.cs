using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    public string nextSceneName; // Nama scene selanjutnya yang dapat diatur melalui Unity Editor

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

        yield return new WaitForSeconds(audioClip.length); // Tunggu hingga audio selesai diputar

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f); // Delay 1 detik

        SceneManager.LoadScene(nextSceneName); // Menggunakan nama scene selanjutnya yang diatur melalui Unity Editor
    }
}
