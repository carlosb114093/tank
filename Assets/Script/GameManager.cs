using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton

    public GameObject victoryText;
    public GameObject loserText;
    
    [SerializeField] private bool isPaused, didLose;
    private Transform doorLevel;
    private ObstacleSpawner obstacleSpawner;    
    private float timeToWin; 

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
        return;
    }
    
    if (loserText != null)
    {
        DontDestroyOnLoad(loserText);
    }
}

    private void Start() {
        obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>(); // Referencia al spawner
    }

    private void Update() {
        if (!didLose)
        {
            timeToWin += Time.deltaTime; // Contador de tiempo si no se ha perdido
        }

        if (timeToWin >= 30f)
        {
            GameOverLose(true);
            timeToWin = 0;
        }
    }
    
public void GameOverLose(bool lose)
{
    if (lose)
    {
        if (loserText != null)
        {
            loserText.SetActive(true);
        }
        StartCoroutine(ReloadScene());
    }
}

private IEnumerator ReloadScene()
{
    yield return new WaitForSeconds(0.5f); // Pequeño retraso
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    public void GameOverWin(bool didWin)
    {
        if (didWin)
        {
            obstacleSpawner.canSpawn = false;
            victoryText.SetActive(true);
            Time.timeScale = 0; // Pausa el juego
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}

