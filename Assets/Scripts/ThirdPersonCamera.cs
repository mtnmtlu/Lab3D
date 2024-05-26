using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Takip edilecek nesne (sphere)
    public Vector3 offset = new Vector3(0, 10, -15); // Kamera ile nesne arasındaki mesafe
    public float moveSpeed = 10f; // Kameranın takip etme hızı

    void LateUpdate()
    {
        if (target == null)
            return;

        // İstenen pozisyonu hesaplama
        Vector3 desiredPosition = target.position + offset;

        // Kamera pozisyonunu hızlı bir şekilde güncelleme
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
