using TMPro;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    // ✅ Singleton: La única instancia accesible globalmente
    public static Countdown Instance { get; private set; }

    public TextMeshProUGUI timeCounter;
    [SerializeField] private float countdownTime = 10f;
    [SerializeField] private GameObject hurryup;
    private bool gameStarted = false;

    void Awake()
    {
        hurryup.SetActive(false);
        // ✅ Verifica si ya hay una instancia
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe un `Countdown`
        }
    }

    void Update()
    {
        if (gameStarted && countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            int timeRemaining = Mathf.CeilToInt(countdownTime);
            timeCounter.text = "Tiempo restante " + timeRemaining;

            if (countdownTime < 6)
            {
                timeCounter.text = "Hurry up! " + timeRemaining;
                hurryup?.SetActive(countdownTime > 0);
            }
        }
    }

    public void StartCountdown()
    {
        gameStarted = true;
        timeCounter.gameObject.SetActive(true);
    }
}
