using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInputText : MonoBehaviour
{
    public TMP_Text inputTextUI; // Referensi ke komponen TMP_Text untuk menampilkan input teks

    private void Start()
    {
        // Mendapatkan nilai input teks dari PlayerPrefs
        string inputText = PlayerPrefs.GetString("InputText");

        // Menampilkan input teks pada TMP_Text
        inputTextUI.text = inputText;

        // Mengatur ulang Time.timeScale menjadi 1
        Time.timeScale = 1f;
    }
}
