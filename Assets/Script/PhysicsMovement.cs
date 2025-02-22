using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical, ascend;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 0f;
    public float flySpeed = 1f; // Velocidad de ascenso/descenso en el eje Y
    [SerializeField] private Rigidbody playerRigidbody;
    private Vector3 moveDirection;
    public bool isGrounded;
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;  
    private bool hasPowerup = false; 
    private bool canJump = false;     
    public GameObject powerupIndicator;
    public GameObject jumpFlame;
    public GameObject tanque; // Referencia al tanque
    public GameObject UFO;
    private bool isControllingTank = true; 
    // Cámara principal
     
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        AudioManager.Instance.PlayMusic(backgroundMusic);
    }

    void Update()
    {
        HandleInput();

        if (moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
        }
    }

    void FixedUpdate()
    {
        ApplyPhysicsMovement();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        ascend = Input.GetAxis("Fly");

        moveDirection = new Vector3(horizontal, ascend, vertical).normalized;

        // Solo permitir el salto si tiene el powerup y está en el suelo
        if (Input.GetKeyDown(KeyCode.E)  && hasPowerup)
        {
            
            ApplyJump(true);
            
            
        }
        if (Input.GetKeyDown(KeyCode.T)  && hasPowerup)
        {
            
            
            SwitchVehicle(true);
            
        }
    }

    private void ApplyPhysicsMovement()
    {
        playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    private void RotateTowardsMovement()
    {
        Vector3 directionWithoutY = new Vector3(moveDirection.x, 0f, moveDirection.z);
        
        if (directionWithoutY != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionWithoutY);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            HandleWin();
        }

        if (other.CompareTag("PowerupJ"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine()); // Iniciar la corrutina correctamente
            Debug.Log("Trigger: Powerup activado");
        }
    }

    void SwitchVehicle(bool controlTank)
    {
        isControllingTank = !isControllingTank;
        SetActiveVehicle(isControllingTank);
    }

    void SetActiveVehicle(bool controlTank)
    {
        // Activar el tanque y desactivar el UFO, o viceversa
        tanque.GetComponent<PhysicsMovement>().enabled = controlTank;
        tanque.GetComponent<Rigidbody>().isKinematic = !controlTank; // Evita colisiones cuando está inactivo

        UFO.GetComponent<PhysicsMovement>().enabled = !controlTank;
        UFO.GetComponent<Rigidbody>().isKinematic = controlTank;
    }

    

    private void ApplyJump(bool canJump)
    {    
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);           
            AudioManager.Instance.PlaySFX(jumpSound);
            
        
    }    

    

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(30);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        Debug.Log("Power up finalizado");
    }

    public void HandleWin()
    {
        GameManager.Instance.GameOverWin(true);  
    }
}



