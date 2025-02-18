using UnityEngine;
using UnityEngine.UI;

public class PushPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 playerGroundPos;
    public float speed = 5f;
    public float jumpForce = 7f; // Fuerza del salto
    private float time;
    private Rigidbody rb;
    private float lastPlayerY;
     public Text loseText;

    void Start()
    {
        // Referencia a la posición del jugador
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        if (playerTransform != null)
        {
            lastPlayerY = playerTransform.position.y; // Guardamos la altura inicial
        }

        // Encuentra el texto "You Lose" en la escena
        if (loseText == null)
        {
            loseText = GameObject.Find("LoseText")?.GetComponent<Text>();
        }

        // Asegura que el texto esté oculto al inicio
        if (loseText != null)
        {
            loseText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Guardar solo coordenadas en X y Z para seguir al jugador
        playerGroundPos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

        // Contador de tiempo para destruir el objeto después de 3 segundos
        time += Time.deltaTime;
        if (time >= 3f)
        {
            Destroy(gameObject);
        }

        // Movimiento del objeto persiguiendo al jugador
        transform.position = Vector3.Lerp(transform.position, playerGroundPos, speed * Time.deltaTime);

        // Detectar si el jugador saltó
        if (playerTransform.position.y > lastPlayerY + 0.1f) // Si el jugador sube en Y
        {
            Jump();
        }

        // Actualizar la última posición en Y del jugador
        lastPlayerY = playerTransform.position.y;
    }

    void Jump()
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset en Y para evitar acumulación
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
} 
