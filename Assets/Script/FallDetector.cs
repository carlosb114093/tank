using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetector : MonoBehaviour
{
    public float fallThreshold = -10f; // Altura en la que consideramos que el objeto ha caído

    private Vector3 initialPosition; // Guardamos la posición inicial para reiniciar

    void Start()
    {
        // Guardamos la posición inicial del objeto
        initialPosition = transform.position;
    }

    void Update()
    {
        // Si el objeto cae por debajo del umbral, tomamos acción
        if (transform.position.y < fallThreshold)
        {
            OnFall();
        }
    }

    void OnFall()
    {
        Debug.Log(gameObject.name + " ha caído al vacío!");

        //Opción 1: Reiniciar la escena
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOverLose(true);
        }
        else
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
    }

        // Opción 2: Devolver el objeto a su posición inicial
        //transform.position = initialPosition;
}        //GetComponent<Rigidbody>().velocity = Vector3.zero; // Detiene cualquier velocidad residual
