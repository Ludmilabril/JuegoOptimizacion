using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OPT_GenerarBotones : MonoBehaviour
{
    [SerializeField] private List<Button> botonesGrid;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonGrid;
    [SerializeField] private OPT_UI_Controller UI_Controller;

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

    //List<GameObject> botonesGrid = new List<GameObject>(); 
    private string[] palabrasEspañol;
    private string[] palabrasIngles;

    private int[] indicesDisponibles;
    private int[] indicesIngles = new int[5];

    private void Awake()
    {
        /*se crean arrays de palabras y de indices disponibles una sola vez, para no tener que crearlos cada vez que
        se genera un nuevo set de botones*/
        palabrasEspañol = new string[wordPairs.Count];
        palabrasIngles = new string[wordPairs.Count];
        indicesDisponibles = new int[wordPairs.Count];

        int i = 0;

        foreach (var pareja in wordPairs)
        {
            palabrasEspañol[i] = pareja.Key;
            palabrasIngles[i] = pareja.Value;
            i++;
        }

        foreach (Button b in botonesGrid)
        {
            b.onClick.AddListener(() => SeleccionarBoton(b));
        }
    }

    public void CallGeneratePairs()
    {
        GeneratePairs();
    }

    private void GeneratePairs()
    {
        reiniciarGrilla();

        //se restablecen los indices sin crear otra colección (List)
        for (int i = 0; i < indicesDisponibles.Length; i++)
        {
            indicesDisponibles[i] = i;
        }

        /* se intercambian las posiciones del indice i con la de las palabras seleccionadas con randomIndex para 
          evitar que se vuelvan a seleccionar las mismas palabras
         */
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(i, indicesDisponibles.Length);

            int temporal = indicesDisponibles[i];
            indicesDisponibles[i] = indicesDisponibles[randomIndex];
            indicesDisponibles[randomIndex] = temporal;

            indicesIngles[i] = indicesDisponibles[i];
        }

        ShuffleIndices(indicesIngles);

        //se llenan los 5 botones de la columna izquierda con las palabras en español
        for (int i = 0; i < 5; i++)
        {
            int indice = indicesDisponibles[i];

            LlenarBoton(botonesGrid[i], palabrasEspañol[indice]);
            botonesGrid[i].interactable = true;
        }

        //se llenan los 5 botones de la columna derecha con las palabras en inglés
        for (int i = 0; i < 5; i++)
        {
            int indice = indicesIngles[i];

            LlenarBoton(botonesGrid[i + 5], palabrasIngles[indice]);
            botonesGrid[i + 5].interactable = true;
        }
    }

    private void ShuffleIndices(int[] indices)
    {
        for (int i = 0; i < indices.Length; i++)
        {
            int randomIndex = Random.Range(i, indices.Length);

            int temporal = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temporal;
        }
    }

    private void LlenarBoton(Button boton, string palabra)
    {
        TMP_Text texto = boton.GetComponentInChildren<TMP_Text>();
        texto.text = palabra;
    }

    private void SeleccionarBoton(Button boton)
    {
        if (botonSeleccionado_Anterior == null)
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

            if (checkGameEnded())
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
        foreach (Button boton in botonesGrid)
        {
            if (boton.interactable)
            {
                return false;
            }
        }

        return true;
    }

    void reiniciarGrilla()
    {
        botonSeleccionado_Anterior = null;
    }
}
