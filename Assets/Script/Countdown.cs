using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI timeCounter; // Referencia al UI TextMeshProUGUI
    [SerializeField]  float countdownTime; // Tiempo inicial del contador
    [SerializeField] private GameObject hurryup;

   void Start(){
    countdownTime = 30f;

   }


    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            int timeRemaining = Mathf.CeilToInt(countdownTime); // Redondear hacia arriba para precisión
            timeCounter.text = "Tiempo restante " + timeRemaining;

            if (countdownTime < 6)
            {
                timeCounter.text = "Te estás quedando sin tiempo " + timeRemaining;

                if (hurryup != null) // Verificar si está asignado
                {
                    if (countdownTime > 0){
                    hurryup.SetActive(true); 
                    }
                    else{
                     hurryup.SetActive(false);   
                    }
                }
                else
                {
                    Debug.LogError("hurryupScreen no ha sido asignado en el Inspector.");
                }
            }
        }
    }
}