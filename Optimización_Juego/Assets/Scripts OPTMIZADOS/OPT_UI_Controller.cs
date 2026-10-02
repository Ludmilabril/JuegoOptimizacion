using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class OPT_UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject menuPrincial;
    [SerializeField] private GameObject wordMatchingGame;
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private TMP_Text txtTimer;
    [SerializeField] private TMP_Text txtPuntaje;
    [SerializeField] private OPT_GenerarBotones generarBotones;

    float time = 0f;
    int puntaje = 0;
    public void Jugar()
    {
        menuPrincial.SetActive(false);
        continueScreen.SetActive(false);
        wordMatchingGame.SetActive(true);
        generarBotones.CallGeneratePairs();

        time = 0f;
    }

    
    public void Salir()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(wordMatchingGame.activeInHierarchy)
        {
            time += Time.deltaTime;
            txtTimer.text = "Tiempo: " + time.ToString("F2");
        }
    }

    public void OpenContinueScreen()
    {
        wordMatchingGame.SetActive(false);
        continueScreen.SetActive(true);

        txtPuntaje.text = "Puntaje: " + calcularPuntaje().ToString();
    }

    int calcularPuntaje()
    {
        double puntuacion = 10 + 50/Math.Sqrt(time);
        return puntaje += (int)puntuacion;
    }
}
