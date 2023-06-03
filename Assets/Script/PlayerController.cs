//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    private CharacterController controller;
//    private Animator animator;
//    private Vector3 playerVelocity;
//    private bool groundedPlayer;
//    private bool isJumpingAllowed = false;
//    private bool isMoving = false;
//    private bool isGrounded = false;
//    private bool isFalling = false;
//    private float playerSpeed = 2.0f;
//    private float jumpHeight = 1.0f;
//    private float gravityValue = -9.81f;
//    private float rotationSpeed = 10f;

//    private void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        animator = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            QuitGame();
//        }
//        groundedPlayer = controller.isGrounded;
//        if (groundedPlayer && playerVelocity.y < 0)
//        {
//            playerVelocity.y = 0f;
//            isGrounded = true;
//            isFalling = false;
//        }
//        else
//        {
//            isGrounded = false;
//        }

//        float horizontalInput = Input.GetAxis("Horizontal");
//        float verticalInput = Input.GetAxis("Vertical");

//        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
//        float inputMagnitude = Mathf.Clamp01(move.magnitude);

//        if (Input.GetKey(KeyCode.LeftShift))
//        {
//            inputMagnitude /= 2;
//            playerSpeed /= 2;
//            isJumpingAllowed = false;
//            isMoving = true;
//        }
//        else
//        {
//            playerSpeed = 2.0f;
//            isJumpingAllowed = true;
//            isMoving = inputMagnitude > 0;
//        }

//        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);
//        animator.SetBool("IsMoving", isMoving);
//        animator.SetBool("IsGrounded", isGrounded);
//        animator.SetBool("IsFalling", isFalling);
//        animator.SetBool("IsJumping", isJumpingAllowed && Input.GetButtonDown("Jump") && groundedPlayer);

//        move = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * move;

//        controller.Move(move * Time.deltaTime * playerSpeed);

//        if (move != Vector3.zero)
//        {
//            Quaternion toRotation = Quaternion.LookRotation(move.normalized);
//            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
//        }

//        if (groundedPlayer)
//        {
//            if (Input.GetButtonDown("Jump") && isJumpingAllowed)
//            {
//                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
//            }
//            else
//            {
//                if (!isJumpingAllowed)
//                {
//                    playerVelocity.y = 0f;
//                }
//            }
//        }
//        else
//        {
//            // Periksa apakah pemain berada di atas objek dengan tag "Ground"
//            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
//            bool isOnGround = false;
//            foreach (Collider collider in hitColliders)
//            {
//                if (collider.CompareTag("Ground"))
//                {
//                    isOnGround = true;
//                    break;
//                }
//            }

//            if (playerVelocity.y < 0 && !isOnGround)
//            {
//                isFalling = true;
//                animator.SetBool("IsFalling", true);
//            }
//            else if (playerVelocity.y >= 0 && isFalling)
//            {
//                isFalling = false;
//                animator.SetBool("IsFalling", false);
//            }
//        }

//        playerVelocity.y += gravityValue * Time.deltaTime;
//        controller.Move(playerVelocity * Time.deltaTime);
//    }

//    void QuitGame()
//    {
//        Application.Quit();
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    private CharacterController controller;
//    private Animator animator;
//    private Vector3 playerVelocity;
//    private bool groundedPlayer;
//    private bool isJumpingAllowed = false;
//    private bool isMoving = false;
//    private bool isGrounded = false;
//    private bool isFalling = false;
//    private float playerSpeed = 2.0f;
//    private float jumpHeight = 1.0f;
//    private float gravityValue = -9.81f;
//    private float rotationSpeed = 10f;

//    private AudioSource audioSource;
//    private AudioSource jumpAudioSource;
//    public AudioClip footstepSound;
//    public AudioClip jumpSound;
//    public float soundEffectSpeedScale = 1.0f;

//    private void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        animator = GetComponent<Animator>();

//        audioSource = GetComponent<AudioSource>();
//        audioSource.clip = footstepSound;

//        jumpAudioSource = gameObject.AddComponent<AudioSource>();
//        jumpAudioSource.clip = jumpSound;
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            QuitGame();
//        }
//        groundedPlayer = controller.isGrounded;
//        if (groundedPlayer && playerVelocity.y < 0)
//        {
//            playerVelocity.y = 0f;
//            isGrounded = true;
//            isFalling = false;
//        }
//        else
//        {
//            isGrounded = false;
//        }

//        float horizontalInput = Input.GetAxis("Horizontal");
//        float verticalInput = Input.GetAxis("Vertical");

//        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
//        float inputMagnitude = Mathf.Clamp01(move.magnitude);

//        if (Input.GetKey(KeyCode.LeftShift))
//        {
//            inputMagnitude /= 2;
//            playerSpeed /= 2;
//            isJumpingAllowed = false;
//            isMoving = true;
//        }
//        else
//        {
//            playerSpeed = 2.0f;
//            isJumpingAllowed = true;
//            isMoving = inputMagnitude > 0;
//        }

//        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);
//        animator.SetBool("IsMoving", isMoving);
//        animator.SetBool("IsGrounded", isGrounded);
//        animator.SetBool("IsFalling", isFalling);
//        animator.SetBool("IsJumping", isJumpingAllowed && Input.GetButtonDown("Jump") && groundedPlayer);

//        move = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * move;

//        controller.Move(move * Time.deltaTime * playerSpeed);

//        if (move != Vector3.zero)
//        {
//            Quaternion toRotation = Quaternion.LookRotation(move.normalized);
//            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
//        }

//        if (groundedPlayer)
//        {
//            if (Input.GetButtonDown("Jump") && isJumpingAllowed)
//            {
//                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
//                jumpAudioSource.PlayOneShot(jumpSound);
//            }
//            else
//            {
//                if (!isJumpingAllowed)
//                {
//                    playerVelocity.y = 0f;
//                }
//            }
//        }
//        else
//        {
//            // Periksa apakah pemain berada di atas objek dengan tag "Ground"
//            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
//            bool isOnGround = false;
//            foreach (Collider collider in hitColliders)
//            {
//                if (collider.CompareTag("Ground"))
//                {
//                    isOnGround = true;
//                    break;
//                }
//            }

//            if (playerVelocity.y < 0 && !isOnGround)
//            {
//                isFalling = true;
//                animator.SetBool("IsFalling", true);
//            }
//            else if (playerVelocity.y >= 0 && isFalling)
//            {
//                isFalling = false;
//                animator.SetBool("IsFalling", false);
//            }
//        }

//        playerVelocity.y += gravityValue * Time.deltaTime;
//        controller.Move(playerVelocity * Time.deltaTime);

//        if (Input.GetKey(KeyCode.LeftShift))
//        {
//            // Menghentikan suara langkah kaki jika tombol Shift ditekan
//            audioSource.Stop();
//        }
//        else
//        {
//            if (isMoving && isGrounded) // Memeriksa apakah player sedang bergerak dan berada di tanah
//            {
//                if (!audioSource.isPlaying)
//                {
//                    audioSource.Play();
//                }
//            }
//            else
//            {
//                audioSource.Stop();
//            }
//        }

//        audioSource.pitch = Time.timeScale * soundEffectSpeedScale * inputMagnitude;
//    }

//    void QuitGame()
//    {
//        Application.Quit();
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isJumpingAllowed = false;
    private bool isMoving = false;
    private bool isGrounded = false;
    private bool isFalling = false;
    private bool isSwimming = false;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 10f;
    private float sinkingDepth = -0.5f; // Variabel untuk mengatur tingkat tenggelam

    private AudioSource audioSource;
    private AudioSource jumpAudioSource;
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public float soundEffectSpeedScale = 1.0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footstepSound;

        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource.clip = jumpSound;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            isGrounded = true;
            isFalling = false;
        }
        else
        {
            isGrounded = false;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(move.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputMagnitude /= 2;
            playerSpeed /= 2;
            isJumpingAllowed = false;
            isMoving = true;
        }
        else
        {
            playerSpeed = 2.0f;
            isJumpingAllowed = true;
            isMoving = inputMagnitude > 0;
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsJumping", isJumpingAllowed && Input.GetButtonDown("Jump") && groundedPlayer);

        move = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * move;

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (groundedPlayer && !isSwimming)
        {
            if (Input.GetButtonDown("Jump") && isJumpingAllowed)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpAudioSource.PlayOneShot(jumpSound);
            }
            else
            {
                if (!isJumpingAllowed)
                {
                    playerVelocity.y = 0f;
                }
            }
        }
        else
        {
            // Periksa apakah pemain berada di atas objek dengan tag "Ground"
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
            bool isOnGround = false;
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Ground"))
                {
                    isOnGround = true;
                    break;
                }
            }

            if (playerVelocity.y < 0 && !isOnGround)
            {
                isFalling = true;
                animator.SetBool("IsFalling", true);
            }
            else if (playerVelocity.y >= 0 && isFalling)
            {
                isFalling = false;
                animator.SetBool("IsFalling", false);
            }
        }

        // Periksa apakah pemain berada di dalam air
        Collider waterCollider = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.2f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Water"))
            {
                waterCollider = collider;
                break;
            }
        }

        if (waterCollider != null)
        {
            isSwimming = true;
            animator.SetBool("IsSwimming", true);

            // Menyebabkan pemain tenggelam setengah badan
            if (transform.position.y > waterCollider.bounds.max.y + sinkingDepth)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = waterCollider.bounds.max.y + sinkingDepth;
                transform.position = newPosition;
            }
        }
        else
        {
            isSwimming = false;
            animator.SetBool("IsSwimming", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Menghentikan suara langkah kaki jika tombol Shift ditekan
            audioSource.Stop();
        }
        else
        {
            if (isMoving && isGrounded && !isSwimming) // Memeriksa apakah player sedang bergerak dan berada di tanah
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }

        audioSource.pitch = Time.timeScale * soundEffectSpeedScale * inputMagnitude;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
