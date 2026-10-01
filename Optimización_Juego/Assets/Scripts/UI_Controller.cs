using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject menuPrincial;
    [SerializeField] private GameObject wordMatchingGame;
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private TMP_Text txtTimer;
    [SerializeField] private GenerarBotones generarBotones;

    float time = 0f;
    public void Jugar()
    {
        menuPrincial.SetActive(false);
        continueScreen.SetActive(false);
        wordMatchingGame.SetActive(true);
        generarBotones.CallGeneratePairs();
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
}
