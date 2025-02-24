using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool isPaused, didLose;    
    private ObstacleSpawner obstacleSpawner;
    private float timeToWin;
    private bool juegoIniciado;

    [Header("Prefabs (Opcional)")]
    [SerializeField] private GameObject redPrefab;
    [SerializeField] private GameObject greenPrefab;
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject green;
    [SerializeField] private string allowedScene = "Orange_presetScene Lite";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de cambio de escena
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        FindGameObjects(); // Buscar o instanciar objetos
        red.SetActive(false);
        green.SetActive(false);
        juegoIniciado=true;
        if (SceneManager.GetActiveScene().name != allowedScene)
        {
            Debug.Log("Este script no se ejecuta en esta escena.");
            enabled = false; // Desactiva el script
        }
        
    }

    private void Update()
    {
        if (juegoIniciado) 
        {
            timeToWin += Time.deltaTime;
        }

        if (timeToWin >= 10f)
        {
            GameOverLose(true);
            timeToWin = 0;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindGameObjects(); // Buscar o instanciar objetos en la nueva escena
    }

    private void FindGameObjects()
    {
        obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();

        // Buscar en la escena actual
        red = GameObject.Find("Red");
        green = GameObject.Find("Green");

        // Si no se encuentran, instanciarlos
        if (red == null && redPrefab != null)
        {
            red = InstantiatePrefab(redPrefab, "Red");
        }

        if (green == null && greenPrefab != null)
        {
            green = InstantiatePrefab(greenPrefab, "Green");
        }
    }

    private GameObject InstantiatePrefab(GameObject prefab, string name)
    {
        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab);
            obj.name = name;
            obj.SetActive(false); // Se activará solo cuando sea necesario
            return obj;
        }
        Debug.LogError($"El prefab {name} no ha sido asignado en el Inspector.");
        return null;
    }

    public void GameOverLose(bool lose)
    {
        if (lose)
        {
            Time.timeScale = 0; // Pausa el juego
            if (red != null)
            {
                red.SetActive(true);
                Debug.Log("Red activado correctamente.");
            }
            else
            {
                Debug.LogError("Red no está asignado en el Inspector.");
            }

            StartCoroutine(ReloadScene());
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // Restaurar tiempo después de recargar
    }

    public void GameOverWin(bool didWin)
    {
        if (didWin)
        {
            if (obstacleSpawner != null)
                obstacleSpawner.canSpawn = false;

            Time.timeScale = 0; // Pausa el juego

            if (green != null)
            {
                green.SetActive(true);
                Debug.Log("Green activado correctamente.");
            }
            else
            {
                Debug.LogError("Green no ha sido asignado en el Inspector.");
            }
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento para evitar errores
    }

    public void StartGame( bool start )
    {
    if(start){
    Time.timeScale = 1; // Inicia el tiempo del juego
    Countdown.Instance.StartCountdown(); 
    juegoIniciado=true;
    }
    }

     public void Reset(bool star)
    {
        // 1. Restablecer variables del juego (opcional)
        // Puedes restablecer aquí cualquier variable que necesites reiniciar, como la puntuación, la vida del jugador, etc.
        // Ejemplo:
        // puntuacion = 0;
        // vidaJugador = 100;

        // 2. Cargar la escena del menú principal
        SceneManager.LoadScene("menu"); // Reemplaza "NombreDeLaEscenaDelMenu" con el nombre de tu escena de menú

        // 3. (Opcional) Si quieres que el juego comience sin pausar al volver al menú
        Time.timeScale = 1;

        Debug.Log("Juego reiniciado y vuelta al menú.");
    }
}



