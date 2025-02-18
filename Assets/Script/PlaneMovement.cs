using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float forwardSpeed = 10f; // Velocidad de avance
    public float acceleration = 2f;  // Aceleración gradual
    public float liftSpeed = 5f;     // Velocidad de ascenso
    public float maxLiftAngle = 15f; // Ángulo máximo de inclinación

    private bool isTakingOff = true;
    private float currentSpeed;
    private float liftAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (isTakingOff)
        {
            // Incrementa la velocidad con el tiempo para simular aceleración
            currentSpeed += acceleration * Time.deltaTime;

            // Movimiento hacia adelante
            transform.position += transform.forward * currentSpeed * Time.deltaTime;

            // Aumenta el ángulo de inclinación hasta el máximo permitido
            if (liftAngle < maxLiftAngle)
            {
                liftAngle += liftSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(-liftAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }

            // Cuando el ángulo es suficiente, empieza a elevarse
            if (liftAngle >= maxLiftAngle / 2)
            {
                transform.position += transform.up * liftSpeed * Time.deltaTime;
            }
        }
    }
}
