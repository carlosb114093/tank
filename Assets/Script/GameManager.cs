//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public GameObject victoryText;
    [SerializeField]bool isPaused, didLose;
    Transform doorLevel;
    ObstacleSpawner ObstacleSpawner;    
    float timeToWin; 
    public GameObject loserText;   
    private void Start() {
     
        //Referenciamos el script que instancia los obstaculos/enemigos/objetos/etc
        ObstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();
        
    }
    private void Update() {
        if(!didLose)
        {
            //Si no perdimos, seguimos contando tiempo
            timeToWin += Time.deltaTime;
        }


        //si pasamos el tiempo limite, perdemos y paramos ejecución ejecutando el GameOverWin()
        if(timeToWin >= 30f)
        {
            GameOverLose(true);
            timeToWin = 0;
        }
        

    }
    
    //Se ejecuta cuando perdemos y hace todo lo referente a ello (Reiniciar la escena, devovler valores a defecto);   
    public void GameOverLose(bool lose)
    {
        if(lose){            
            //Esta línea me permite cargar scenes de unity.
            loserText.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    //Se ejecuta cuando ganamos y hace todo lo referente a ello (cargar neuvo nivel, darnos feedback, etc);   
    public void GameOverWin(bool didWin)
    {
        if(didWin)
        {
            ObstacleSpawner.canSpawn = false;
            victoryText.SetActive(true); // Activa el mensaje
            Time.timeScale = 0; // Pausa el juego
          
        }
    }

    //Método para pausar el juego
     void PauseGame()
    {
        isPaused = !isPaused;
        
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
}    

