using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI timeCounter; // Referencia al UI TextMeshProUGUI
    [SerializeField] private float countdownTime = 30f; // Tiempo inicial del contador

    void Update()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            int timeRemaining = Mathf.CeilToInt(countdownTime); // Redondear hacia arriba para precisión

            // Verifica si quedan 5 segundos o menos y cambia el formato
            if (timeRemaining <= 5)
            {
                timeCounter.text = $"⚠️ {timeRemaining} ⚠️"; // Resaltado para mayor impacto
            }
            else
            {
                timeCounter.text = $"Tiempo Restante: {timeRemaining}";
            }
        }
        else
        {
            //gameManager.GameOverWin(true);
        }
    }
}