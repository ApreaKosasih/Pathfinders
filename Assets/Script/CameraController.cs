using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Transform objek yang menjadi titik pusat pergerakan kamera
    public float rotationSpeed = 1f; // Kecepatan rotasi kamera
    public float orbitDistance = 10f; // Jarak kamera dari objek target
    public float yOffset = 2f; // Jarak vertikal kamera dari objek target
    public LayerMask obstacleMask; // Layer mask untuk mendeteksi objek penghalang

    private Vector3 offset; // Offset antara kamera dan objek target

    private void Start()
    {
        // Menghitung offset awal antara kamera dan objek target
        offset = new Vector3(0, yOffset, -orbitDistance);
    }

    private void Update()
    {
        // Menghitung posisi baru kamera berdasarkan waktu
        float time = Time.time;
        Quaternion rotation = Quaternion.Euler(0, rotationSpeed * time, 0);
        Vector3 desiredOffset = rotation * offset;

        // Menentukan apakah ada objek penghalang antara kamera dan objek target
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredOffset, out hit, orbitDistance, obstacleMask))
        {
            // Menghindari objek penghalang dengan menggeser posisi kamera
            Vector3 avoidanceDirection = Vector3.ProjectOnPlane(-hit.normal, desiredOffset.normalized);
            desiredOffset = Vector3.Lerp(desiredOffset, avoidanceDirection * orbitDistance, Time.deltaTime);
        }

        // Menetapkan posisi kamera berdasarkan objek target dan offset
        transform.position = target.position + desiredOffset;

        // Menghadapkan kamera ke objek target
        transform.LookAt(target.position);
    }
}
