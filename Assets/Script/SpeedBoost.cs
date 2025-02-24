using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedBoost : MonoBehaviour
{
    public float speedMultiplier = 2f; // Factor de aumento de velocidad
    public float boostDuration = 10f; // Duración del efecto en segundos
    private float originalSpeed;
    private bool isBoosted = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = rb.velocity.magnitude; // Guarda la velocidad inicial
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && !isBoosted)
        {
            StartCoroutine(BoostSpeed());
            Destroy(other.gameObject); // Destruir el power-up tras recogerlo
        }
    }

    private System.Collections.IEnumerator BoostSpeed()
    {
        isBoosted = true;
        rb.velocity *= speedMultiplier; // Aumenta la velocidad
        yield return new WaitForSeconds(boostDuration);
        rb.velocity = rb.velocity.normalized * originalSpeed; // Restablece la velocidad
        isBoosted = false;
    }
}

