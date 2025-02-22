using UnityEngine;
using System.Collections;
public class FollowTarget : MonoBehaviour
{
    public string targetTag = "Player"; // Etiqueta del objeto a seguir
    public Vector3 offset = new Vector3(0, 5, -10); // Desplazamiento de la cámara
    private Transform target;
    public Camera mainCamera; 
    public GameObject tanque; // Referencia al tanque
    public GameObject UFO;
    private bool isControllingTank = true; 

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta " + targetTag);
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target); // La cámara mira al objetivo
        }

        void UpdateCameraFocus() 
    { 
        Transform target = isControllingTank ? tanque.transform : UFO.transform;
         StartCoroutine(SmoothCameraTransition(target)); 
    }     

    IEnumerator SmoothCameraTransition(Transform target)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, target.position + new Vector3(0, 5, -10), elapsedTime / duration);
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(target.position - mainCamera.transform.position), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar la posición final exacta
        mainCamera.transform.position = target.position + new Vector3(0, 5, -10);
        mainCamera.transform.LookAt(target);
    }
    }
}
