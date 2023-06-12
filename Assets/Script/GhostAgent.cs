using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAgent : MonoBehaviour
{
    public float detectRange = 30f;
    public float followRange = 25f;
    public float fleeRange = 10f;
    public float fleeMaxDistance = 35f; // Jarak maksimal Ghost Agent berlari menjauhi player
    public float movementSpeed = 2f;
    public float fleeSpeedMultiplier = 2f;
    public float randomMovementRadius = 10f;
    public float rotationSpeed = 5f; // Kecepatan rotasi halus

    public AudioSource audioSource; // Komponen AudioSource untuk memutar audio

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Vector3 randomTargetPosition;
    private bool isFollowingPlayer = false;
    private bool isFleeing = false;
    private bool hasFledMaxDistance = false; // Menandakan apakah Ghost Agent telah mencapai jarak maksimal fleeRange

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;

    private bool isShiftKeyPressed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        randomTargetPosition = GetRandomTargetPosition();
    }

    private void Update()
    {
        // Menghitung jarak antara Ghost Agent dan player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShiftKeyPressed = true;
            detectRange -= 10f;
            followRange -= 10f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isShiftKeyPressed = false;
            detectRange += 10f;
            followRange += 10f;
        }

        if (distanceToPlayer <= detectRange)
        {
            // Player berada dalam area deteksi
            isFollowingPlayer = true;

            if (!isFleeing)
            {
                if (distanceToPlayer > followRange)
                {
                    // Player berada di luar jarak followRange, Ghost Agent mendekati player
                    navMeshAgent.speed = movementSpeed / 2;
                    navMeshAgent.SetDestination(player.position);
                    animator.SetFloat("InputMagnitude", navMeshAgent.velocity.magnitude / movementSpeed);
                    StartRotationTransition(player.position - transform.position);
                }
                else if (distanceToPlayer <= fleeRange)
                {
                    // Player mendekati Ghost Agent, Ghost Agent berlari menjauh
                    isFleeing = true;
                    navMeshAgent.speed = movementSpeed * fleeSpeedMultiplier;
                    Vector3 fleeDirection = transform.position - player.position;
                    Vector3 fleeTarget = transform.position + fleeDirection.normalized * detectRange;
                    navMeshAgent.SetDestination(fleeTarget);
                    animator.SetFloat("InputMagnitude", 1f);
                }
                else
                {
                    // Player berada dalam jarak followRange, Ghost Agent mengikuti player
                    navMeshAgent.speed = movementSpeed;
                    navMeshAgent.SetDestination(player.position);
                    animator.SetFloat("InputMagnitude", navMeshAgent.velocity.magnitude / movementSpeed);
                }
            }
            else
            {
                // Ghost Agent dalam keadaan menjauh dari player
                if (!hasFledMaxDistance)
                {
                    // Ghost Agent belum mencapai jarak maksimal fleeRange
                    if (distanceToPlayer > fleeMaxDistance)
                    {
                        // Ghost Agent mencapai jarak maksimal fleeRange, kembali ke keadaan random
                        isFleeing = false;
                        hasFledMaxDistance = true;
                        randomTargetPosition = GetRandomTargetPosition();
                        navMeshAgent.SetDestination(randomTargetPosition);
                        animator.SetFloat("InputMagnitude", 0.5f);
                    }
                }
                else
                {
                    // Ghost Agent telah mencapai jarak maksimal fleeRange
                    if (distanceToPlayer <= detectRange)
                    {
                        // Player masuk kembali ke area deteksi, Ghost Agent kembali mengikuti player
                        isFleeing = false;
                        hasFledMaxDistance = false;
                        navMeshAgent.speed = movementSpeed / 2;
                        navMeshAgent.SetDestination(player.position);
                        animator.SetFloat("InputMagnitude", navMeshAgent.velocity.magnitude / movementSpeed);
                        StartRotationTransition(player.position - transform.position);
                    }
                }
            }
        }
        else
        {
            // Player berada di luar area deteksi, Ghost Agent bergerak secara random
            isFollowingPlayer = false;
            isFleeing = false;
            hasFledMaxDistance = false;
            navMeshAgent.speed = movementSpeed / 2;
            navMeshAgent.SetDestination(randomTargetPosition);
            animator.SetFloat("InputMagnitude", 0.5f);
        }

        if (isRotating)
        {
            TransitionRotation();
        }

        // Putar audio jika Ghost Agent sedang mengikuti player
        if (isFollowingPlayer)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private Vector3 GetRandomTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomMovementRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, randomMovementRadius, 1);
        return hit.position;
    }

    private void StartRotationTransition(Vector3 targetDirection)
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.LookRotation(targetDirection);
        isRotating = true;
    }

    private void TransitionRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isRotating = false;
        }
    }
}





//using UnityEngine;
//using UnityEngine.AI;

//public class GhostAgent : MonoBehaviour
//{
//    public float detectRange = 30f;
//    public float followRange = 25f;
//    public float fleeRange = 10f;
//    public float fleeMaxDistance = 35f; // Jarak maksimal Ghost Agent berlari menjauhi player
//    public float movementSpeed = 2f;
//    public float fleeSpeedMultiplier = 2f;
//    public float randomMovementRadius = 10f;

//    private Animator animator;
//    private NavMeshAgent navMeshAgent;
//    private Transform player;
//    private Vector3 randomTargetPosition;
//    private bool isFollowingPlayer = false;
//    private bool isFleeing = false;
//    private bool hasFledMaxDistance = false; // Menandakan apakah Ghost Agent telah mencapai jarak maksimal fleeRange

//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//        navMeshAgent = GetComponent<NavMeshAgent>();
//        player = GameObject.FindGameObjectWithTag("Player").transform;
//        randomTargetPosition = GetRandomTargetPosition();
//    }

//    private void Update()
//    {
//        // Menghitung jarak antara Ghost Agent dan player
//        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

//        if (distanceToPlayer <= detectRange)
//        {
//            // Player berada dalam area deteksi
//            isFollowingPlayer = true;

//            if (!isFleeing)
//            {
//                if (distanceToPlayer > followRange)
//                {
//                    // Player berada di luar jarak followRange, Ghost Agent mendekati player
//                    navMeshAgent.speed = movementSpeed / 2;
//                    navMeshAgent.SetDestination(player.position);
//                    animator.SetFloat("InputMagnitude", navMeshAgent.velocity.magnitude / movementSpeed);
//                }
//                else if (distanceToPlayer <= fleeRange)
//                {
//                    // Player mendekati Ghost Agent, Ghost Agent berlari menjauh
//                    isFleeing = true;
//                    navMeshAgent.speed = movementSpeed * fleeSpeedMultiplier;
//                    Vector3 fleeDirection = transform.position - player.position;
//                    Vector3 fleeTarget = transform.position + fleeDirection.normalized * detectRange;
//                    navMeshAgent.SetDestination(fleeTarget);
//                    animator.SetFloat("InputMagnitude", 1f);
//                }
//                else
//                {
//                    // Player berada dalam jarak followRange, Ghost Agent mengikuti player
//                    navMeshAgent.speed = movementSpeed;
//                    navMeshAgent.SetDestination(player.position);
//                    animator.SetFloat("InputMagnitude", navMeshAgent.velocity.magnitude / movementSpeed);
//                }
//            }
//            else
//            {
//                // Ghost Agent dalam keadaan menjauh dari player
//                if (!hasFledMaxDistance)
//                {
//                    // Ghost Agent belum mencapai jarak maksimal fleeRange
//                    if (distanceToPlayer > fleeMaxDistance)
//                    {
//                        // Ghost Agent mencapai jarak maksimal fleeRange, kembali ke keadaan random
//                        isFleeing = false;
//                        hasFledMaxDistance = true;
//                        randomTargetPosition = GetRandomTargetPosition();
//                        navMeshAgent.SetDestination(randomTargetPosition);
//                        animator.SetFloat("InputMagnitude", 0.5f);
//                    }
//                }
//                else
//                {
//                    // Ghost Agent telah mencapai jarak maksimal fleeRange
//                    if (distanceToPlayer <= detectRange)
//                    {
//                        // Player masuk kembali ke area deteksi, Ghost Agent kembali mengikuti player
//                        isFleeing = false;
//                        hasFledMaxDistance = false;
//                        navMeshAgent.speed = movementSpeed / 2;
//                        navMeshAgent.SetDestination(player.position);
//                        animator.SetFloat("InputMagnitude", navMeshAgent.velocity.magnitude / movementSpeed);
//                    }
//                }
//            }
//        }
//        else
//        {
//            // Player berada di luar area deteksi, Ghost Agent bergerak secara random
//            isFollowingPlayer = false;
//            isFleeing = false;
//            hasFledMaxDistance = false;
//            navMeshAgent.speed = movementSpeed / 2;
//            navMeshAgent.SetDestination(randomTargetPosition);
//            animator.SetFloat("InputMagnitude", 0.5f);
//        }
//    }

//    private Vector3 GetRandomTargetPosition()
//    {
//        Vector3 randomDirection = Random.insideUnitSphere * randomMovementRadius;
//        randomDirection += transform.position;
//        NavMeshHit hit;
//        NavMesh.SamplePosition(randomDirection, out hit, randomMovementRadius, 1);
//        return hit.position;
//    }
//}
