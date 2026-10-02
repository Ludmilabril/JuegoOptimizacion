using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GenerarBotones : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonGrid;
    [SerializeField] private UI_Controller UI_Controller;

    private Button botonSeleccionado_Anterior;

    Dictionary<string, string> wordPairs = new Dictionary<string, string>()
    {
        { "Perro", "Dog" },
        { "Gato", "Cat" },
        { "León", "Lion" },
        { "Jirafa", "Giraffe" },
        { "Pez", "Fish" },
        { "Ave", "Bird" },
        { "Elefante", "Elephant" },
        { "Hormiga", "Ant" },
        { "Tigre", "Tiger" },
        { "Oso", "Bear" },
        { "Vaca", "Cow" },
        { "Cabra", "Goat" },
        { "Tiburón", "Shark" },
        { "Ballena", "Whale" },
        { "Delfín", "Dolphin" },
        { "Pingüino", "Penguin" },
        { "Tortuga", "Turtle" },
        { "Rana", "Frog" },
        { "Ratón", "Mouse" },
        { "Gallina", "Chicken" },
        { "Cerdo", "Pig" }
    };

    List<GameObject> botonesGrid = new List<GameObject>();

    public void CallGeneratePairs()
    {
         GeneratePairs();
    }

    private void GeneratePairs()
    {
        reiniciarGrilla();

        List<string> palabrasDisponibles_Español = new List<string>(wordPairs.Keys);

        List<string> palabras_Español = new List<string>();
        List<string> palabras_Ingles = new List<string>();

        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, palabrasDisponibles_Español.Count);

            string spanishWord = palabrasDisponibles_Español[randomIndex];
            string englishWord = wordPairs[spanishWord];

            palabrasDisponibles_Español.RemoveAt(randomIndex);

            palabras_Español.Add(spanishWord);
            palabras_Ingles.Add(englishWord);
        }

        Shuffle(palabras_Ingles);

        foreach (string word in palabras_Español)
        {
            GameObject button = Instantiate(buttonPrefab, buttonGrid);

            Button boton = button.GetComponent<Button>();
            boton.onClick.AddListener(() => SeleccionarBoton(boton));

            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = word;

            botonesGrid.Add(button);
        }

        foreach (string word in palabras_Ingles)
        {
            GameObject button = Instantiate(buttonPrefab, buttonGrid);

            Button boton = button.GetComponent<Button>();
            boton.onClick.AddListener(() => SeleccionarBoton(boton));

            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = word;

            botonesGrid.Add(button);
        }
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);

            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void SeleccionarBoton(Button boton)
    {
        if(botonSeleccionado_Anterior == null)
        {
            botonSeleccionado_Anterior = boton;
            return;
        }

        string txtBotonPrevio = botonSeleccionado_Anterior.GetComponentInChildren<TMP_Text>().text;
        string txtBotonActual = boton.GetComponentInChildren<TMP_Text>().text;

        if (wordPairs.ContainsKey(txtBotonPrevio) && wordPairs[txtBotonPrevio] == txtBotonActual ||
            wordPairs.ContainsKey(txtBotonActual) && wordPairs[txtBotonActual] == txtBotonPrevio)
        {
            botonSeleccionado_Anterior.interactable = false;
            boton.interactable = false;
            botonSeleccionado_Anterior = null;

            if(checkGameEnded())
            {
                UI_Controller.OpenContinueScreen();
            }
        }
        else
        {
            botonSeleccionado_Anterior = boton;
        }
    }

    bool checkGameEnded()
    {
        return botonesGrid.All(item => !item.GetComponent<Button>().interactable);
    }

    void reiniciarGrilla()
    {
        foreach (var item in botonesGrid)
        {
            Destroy(item);
        }
        botonesGrid.Clear();

        botonSeleccionado_Anterior = null;
    }
}
