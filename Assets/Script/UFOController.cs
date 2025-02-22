using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class UFOController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float minHeight = 1.5f; // Altura mínima del UFO
    public float hoverForce = 10f; // Fuerza de levitación
    public float hoverDamping = 5f; // Suaviza la estabilización
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
    }

    void FixedUpdate()
    {
        MaintainHeight();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Acceleration);

        // Rotar hacia la dirección del movimiento
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void MaintainHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            float distanceToGround = hit.distance;

            if (distanceToGround < minHeight)
            {
                float liftForce = (minHeight - distanceToGround) * hoverForce;
                rb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
            }
        }
    }
}

