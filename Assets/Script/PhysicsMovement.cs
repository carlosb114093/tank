using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical, ascend;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    public float flySpeed = 3f; // Velocidad de ascenso/descenso en el eje Y
    [SerializeField] private Rigidbody playerRigidbody;
    private Vector3 moveDirection;
    public bool isGrounded;
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
    
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
        // Capturar entrada en los 3 ejes
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        ascend = Input.GetAxis("Fly"); // Nuevo input para moverse en Y (ej. tecla espacio para subir)

        moveDirection = new Vector3(horizontal, ascend, vertical).normalized;
        
    }

    private void ApplyPhysicsMovement()
    {
        // Movimiento en los 3 ejes
        playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void ApplyJump()
    {
        // Aplicar salto con física
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
         AudioManager.Instance.PlaySFX(jumpSound);
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
        // Asegurar que la rotación no afecta el eje Y si el jugador está flotando
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
    }

    public void HandleWin()
    {
      GameManager.Instance.GameOverWin(true);  
        
    }
}
