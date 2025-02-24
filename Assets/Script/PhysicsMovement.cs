using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical, ascend;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 0f;
    public float flySpeed = 1f; 
    public float minHeight = 1.5f; // Altura mínima del UFO
    public float hoverForce = 10f; // Fuerza de levitación
    public float hoverDamping = 5f; // Suaviza la estabilización
    [SerializeField] private Rigidbody playerRigidbody;
    private Vector3 moveDirection;
    public bool isGrounded;
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;  
    private bool hasPowerup = false;            
    public float speedMultiplier = 2f; // Factor de aumento de velocidad
    public float boostDuration = 10f; // Duración del efecto en segundos
    private float originalSpeed;
    private bool isBoosted = false;
 
     
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();         
        AudioManager.Instance.PlayMusic(backgroundMusic);
        originalSpeed = playerRigidbody.velocity.magnitude;
        
          
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
         MaintainHeight();
        void FixedUpdate()
{
    float groundDistance = 1.5f; // Distancia mínima del suelo
    RaycastHit hit;

    if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance))
    {
        // Si el UFO está muy cerca del suelo, no lo dejes bajar más
        if (hit.collider.CompareTag("Floor"))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = hit.point.y + groundDistance; // Mantiene una distancia segura del suelo
            transform.position = newPosition;
        }
    }
} 
    }

    private void HandleInput()
    {
        //asigno controles para los 3 ejes
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        ascend = Input.GetAxis("Fly");

        //guardo el movimiento en un vector
        moveDirection = new Vector3(horizontal, ascend, vertical).normalized;

        // Solo permitir el salto si tiene el powerup 
        if (Input.GetKeyDown(KeyCode.E)  && hasPowerup)        
        {
            
            ApplyJump(true);
            
            
        }

        if (Input.GetKeyDown(KeyCode.P))        
        {
            
            GameManager.Instance.PauseGame();
                      
            
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
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine()); // Iniciar la corrutina correctamente
            Debug.Log("Trigger: Powerup activado");
        }
        if (other.CompareTag("PowerUp") && !isBoosted)
        {
            StartCoroutine(BoostSpeed());
            Destroy(other.gameObject); // Destruir el power-up tras recogerlo
        }

    }

    private System.Collections.IEnumerator BoostSpeed()
    {
        isBoosted = true;
        playerRigidbody.velocity *= speedMultiplier; // Aumenta la velocidad
        yield return new WaitForSeconds(boostDuration);
        playerRigidbody.velocity = playerRigidbody.velocity.normalized * originalSpeed; // Restablece la velocidad
        isBoosted = false;
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
        Debug.Log("Power up finalizado");
    }

    public void HandleWin()
    {
        GameManager.Instance.GameOverWin(true);  
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
                playerRigidbody.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
                Debug.Log("Raycast");
            }
        }
    }
}



