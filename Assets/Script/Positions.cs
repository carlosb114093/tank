using UnityEngine;
using System.Collections;

public class Positions : MonoBehaviour
{
    public GameObject tanque; // Referencia al tanque
    public GameObject UFO;
    private bool isControllingTank = true; 
public void SwitchVehicle()
    {
        isControllingTank = !isControllingTank; // Alternar control entre tanque y UFO
        SwapPositions(); // Intercambiar posiciones y rotaciones
        SetActiveVehicle(isControllingTank); // Activar el vehículo seleccionado
    }

public void SetActiveVehicle(bool controlTank)
    {
        // Activar el tanque y desactivar el UFO, o viceversa
        tanque.GetComponent<PhysicsMovement>().enabled = controlTank;
        tanque.GetComponent<Rigidbody>().isKinematic = !controlTank; // Evita colisiones cuando está inactivo

        UFO.GetComponent<PhysicsMovement>().enabled = !controlTank;
        UFO.GetComponent<Rigidbody>().isKinematic = controlTank;
    }

public void SwapPositions()
    {
        // Guardar posiciones y rotaciones actuales
        Vector3 tempPosition = tanque.transform.position;
        Quaternion tempRotation = tanque.transform.rotation;

        // Intercambiar posiciones y rotaciones
        tanque.transform.position = UFO.transform.position;
        tanque.transform.rotation = UFO.transform.rotation;

        UFO.transform.position = tempPosition;
        UFO.transform.rotation = tempRotation;
    }
}    