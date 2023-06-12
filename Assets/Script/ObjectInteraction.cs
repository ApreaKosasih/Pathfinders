//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class ObjectInteraction : MonoBehaviour
//{
//    public float interactionRange = 2f; // Jarak untuk berinteraksi dengan objek
//    public string nextSceneName; // Nama scene selanjutnya

//    private GameObject player; // Referensi ke pemain

//    private void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//    }

//    private void Update()
//    {
//        // Menghitung jarak antara objek dan pemain
//        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

//        // Memeriksa apakah jarak sesuai untuk berinteraksi
//        if (distanceToPlayer <= interactionRange)
//        {
//            // Memeriksa apakah pemain menekan tombol "E"
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                // Memindahkan ke scene selanjutnya
//                SceneManager.LoadScene(nextSceneName);
//            }
//        }
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class ObjectInteraction : MonoBehaviour
//{
//    public float interactionRange = 2f; // Jarak untuk berinteraksi dengan objek
//    public string nextSceneName; // Nama scene selanjutnya
//    public Canvas canvas; // Komponen Canvas yang berisi elemen-elemen UI

//    private GameObject player; // Referensi ke pemain

//    private bool isInteractable = false; // Menandakan apakah objek dapat diinteraksi

//    private void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        canvas.gameObject.SetActive(false); // Menonaktifkan Canvas saat awal
//    }

//    private void Update()
//    {
//        // Menghitung jarak antara objek dan pemain
//        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

//        // Memeriksa apakah jarak sesuai untuk berinteraksi
//        if (distanceToPlayer <= interactionRange)
//        {
//            // Memeriksa apakah pemain menekan tombol "E"
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                // Memindahkan ke scene selanjutnya
//                SceneManager.LoadScene(nextSceneName);
//            }

//            // Menampilkan Canvas saat objek dapat diinteraksi
//            if (!isInteractable)
//            {
//                canvas.gameObject.SetActive(true);
//                isInteractable = true;
//            }
//        }
//        else
//        {
//            // Menyembunyikan Canvas saat objek tidak dapat diinteraksi
//            if (isInteractable)
//            {
//                canvas.gameObject.SetActive(false);
//                isInteractable = false;
//            }
//        }
//    }
//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class ObjectInteraction : MonoBehaviour
//{
//    public float interactionRange = 2f; // Jarak untuk berinteraksi dengan objek
//    public GameObject canvasObject; // Referensi ke game object canvas
//    public string nextSceneName; // Nama scene selanjutnya

//    private GameObject player; // Referensi ke pemain
//    private bool isInRange = false; // Status pemain berada dalam jarak interaksi
//    private InputField inputField; // Referensi ke komponen InputField

//    private void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        canvasObject.SetActive(false); // Menghilangkan canvas saat permainan dimulai

//        // Mendapatkan referensi ke komponen InputField
//        inputField = canvasObject.GetComponentInChildren<InputField>();
//    }

//    private void Update()
//    {
//        // Menghitung jarak antara objek dan pemain
//        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

//        // Memeriksa apakah pemain berada dalam jarak interaksi
//        if (distanceToPlayer <= interactionRange)
//        {
//            isInRange = true;
//        }
//        else
//        {
//            isInRange = false;
//        }

//        // Memeriksa apakah pemain menekan tombol "E" ketika berada dalam jarak interaksi dan canvas tidak aktif
//        if (isInRange && Input.GetKeyDown(KeyCode.E) && !canvasObject.activeSelf)
//        {
//            // Menampilkan canvas dan menjeda permainan
//            canvasObject.SetActive(true);
//            Time.timeScale = 0f;
//        }

//        // Memeriksa apakah canvas aktif dan pemain menekan tombol "Cancel" (Escape)
//        if (canvasObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
//        {
//            // Menyembunyikan canvas dan melanjutkan permainan
//            canvasObject.SetActive(false);
//            Time.timeScale = 1f;
//        }
//    }

//    public void SaveInputText()
//    {
//        // Menyimpan nilai input teks menggunakan PlayerPrefs
//        string inputText = inputField.text;
//        PlayerPrefs.SetString("InputText", inputText);

//        // Memindahkan ke scene selanjutnya
//        SceneManager.LoadScene(nextSceneName);
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // Jarak untuk berinteraksi dengan objek
    public GameObject canvasObject; // Referensi ke game object canvas
    public GameObject interactableObject; // Referensi ke game object yang ingin ditampilkan/sembunyikan
    public string nextSceneName; // Nama scene selanjutnya

    private GameObject player; // Referensi ke pemain
    private bool isInRange = false; // Status pemain berada dalam jarak interaksi
    private InputField inputField; // Referensi ke komponen InputField

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvasObject.SetActive(false); // Menghilangkan canvas saat permainan dimulai
        interactableObject.SetActive(false); // Menyembunyikan objek saat permainan dimulai

        // Mendapatkan referensi ke komponen InputField
        inputField = canvasObject.GetComponentInChildren<InputField>();
    }

    private void Update()
    {
        // Menghitung jarak antara objek dan pemain
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Memeriksa apakah pemain berada dalam jarak interaksi
        if (distanceToPlayer <= interactionRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }

        // Memeriksa apakah pemain menekan tombol "E" ketika berada dalam jarak interaksi dan canvas tidak aktif
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !canvasObject.activeSelf)
        {
            // Menampilkan canvas dan menjeda permainan
            canvasObject.SetActive(true);
            Time.timeScale = 0f;
        }

        // Memeriksa apakah canvas aktif dan pemain menekan tombol "Cancel" (Escape)
        if (canvasObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            // Menyembunyikan canvas dan melanjutkan permainan
            canvasObject.SetActive(false);
            Time.timeScale = 1f;
        }

        // Memeriksa apakah objek berada dalam jarak interaksi dan objek belum ditampilkan
        if (isInRange && !interactableObject.activeSelf)
        {
            interactableObject.SetActive(true); // Menampilkan objek
        }
        // Memeriksa apakah objek tidak berada dalam jarak interaksi dan objek sedang ditampilkan
        else if (!isInRange && interactableObject.activeSelf)
        {
            interactableObject.SetActive(false); // Menyembunyikan objek
        }
    }

    public void SaveInputText()
    {
        // Menyimpan nilai input teks menggunakan PlayerPrefs
        string inputText = inputField.text;
        PlayerPrefs.SetString("InputText", inputText);

        // Memindahkan ke scene selanjutnya
        SceneManager.LoadScene(nextSceneName);
    }
}
