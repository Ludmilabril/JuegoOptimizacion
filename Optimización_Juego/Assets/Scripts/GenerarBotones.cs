using TMPro;
using UnityEngine;

public class GenerarBotones : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonGrid;

    public void CallGeneratePairs()
    {
         GeneratePairs();
    }

    private void GeneratePairs()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonGrid);

            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = "Botón " + (i + 1);
        }
    }

    //metodo de condicion de victoria
}
