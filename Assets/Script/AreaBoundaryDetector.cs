using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaBoundaryDetector : MonoBehaviour
{
    private bool hasExited = false;
    
    // Límites del área permitida
    public Vector3 boundaryMin = new Vector3(-99f, -1f, -15f); 
    public Vector3 boundaryMax = new Vector3(110f, 30f, 180f);
    private Vector3 initialPosition; // Posición inicial del tanque

    void Start()
    {
        // Guardamos la posición inicial del tanque
        initialPosition = transform.position;
    }

    void Update()
    {
        // Verificamos si el tanque ha salido de los límites
        if (IsOutsideBoundary())
        {
            OnExitBoundary();
        }
    }

    bool IsOutsideBoundary()
    {
    return transform.position.x < boundaryMin.x || transform.position.x > boundaryMax.x ||
           transform.position.z < boundaryMin.z || transform.position.z > boundaryMax.z;
    }


    void OnExitBoundary()
    {
        if (hasExited) return; // Evita múltiples llamadas
        hasExited = true;

        Debug.Log(gameObject.name + " ha salido del área permitida. Reiniciando posición...");

        // Devuelve el tanque a su posición inicial
        transform.position = initialPosition;

        // Opcional: Reinicia la rotación para evitar giros extraños
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        hasExited = false; // Permite detectar futuras salidas

        
    }
}
     

