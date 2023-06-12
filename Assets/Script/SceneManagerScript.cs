using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public AudioClip audioClip;
    //public string subtitleText;
    private AudioSource audioSource;
    public string nextSceneName; // Nama scene selanjutnya yang dapat diatur melalui Unity Editor

    public Animator transition;
    //public Text subtitleTextObject; // Objek teks untuk menampilkan subtitle

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

        //subtitleTextObject.text = subtitleText; // Mengatur teks subtitle

        yield return new WaitForSeconds(audioClip.length); // Tunggu hingga audio selesai diputar

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f); // Delay 1 detik

        SceneManager.LoadScene(nextSceneName); // Menggunakan nama scene selanjutnya yang diatur melalui Unity Editor
    }
}
