using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;


public class Main_ : MonoBehaviour
{
    //cargar archivo

    public string csvFilePath = "Assets/Resources/POSP_data.csv";



    public float timer;
    float timerStop;
    public string[] name_d, value_d, group_d, control_d;




    //BotonGenerador
    public GameObject buttonPrefab; // Prefab del botón
    public GameObject buttonParent; // GameObject padre de los botones
    public bool paso = true;
    public GameObject groupPrefab;//panel de prefab

    //ControlBoton
    public GameObject objetoPadre; // Asigna el objeto padre en el inspector
    public Button[] arregloBotones; // Arreglo para almacenar los botones
    public bool oneShot = true;


    //colores
    Color Naranja;

    public RectTransform parentObject; // Referencia al objeto padre asignada desde el inspector
    public Dictionary<string, GameObject> groupObjects = new Dictionary<string, GameObject>();
    public int columns = 3; // Número de columnas para los botones de categoría
    public float spacing = 10f;

    public RectTransform buttonParentFood; // El área donde se generarán los botones de categoría
    public GameObject buttonPrefabFood; // Prefab del botón a utilizar
   
    public int buttonColumns = 3; // Número de columnas para los botones de categoría
    public float buttonSpacing = 10f; // Espaciado entre los botones de categoría
    public Vector2 buttonSize = new Vector2(100f, 100f); // Tamaño de los botones de categoría

    private bool buttonsGenerated = false; // Para asegurarse de que los botones se generen solo una vez
    //

    void Start()
    {
        Naranja = new Color(255, 120, 0);
        FillArrays();

    }

    private void FillArrays()
    {
        // Verificamos que el archivo CSV exista
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("El archivo CSV no se encuentra en la ruta especificada: " + csvFilePath);
            return;
        }

        // Leemos todas las líneas del archivo CSV
        string[] lines = File.ReadAllLines(csvFilePath);

        // Inicializamos los arreglos basados en el número de líneas (excluyendo la línea de encabezados)
        name_d = new string[lines.Length - 1];
        value_d = new string[lines.Length - 1];
        group_d = new string[lines.Length - 1];
        control_d = new string[lines.Length - 1];

        // Procesamos cada línea (excluyendo la línea de encabezados)
        for (int i = 1; i < lines.Length; i++)
        {
            // Separamos los valores por punto y coma
            string[] valuesInLine = lines[i].Split(';');

            // Verificamos que haya suficientes valores en la línea
            if (valuesInLine.Length < 4)
            {
                Debug.LogError("La línea " + i + " no tiene suficientes valores.");
                continue;
            }

            // Asignamos los valores a los arreglos correspondientes
            name_d[i - 1] = valuesInLine[0].Trim();
            value_d[i - 1] = valuesInLine[1].Trim();
            group_d[i - 1] = valuesInLine[2].Trim();
            control_d[i - 1] = valuesInLine[3].Trim();
        }
    }


    //Boton Generador
    void GenerateButtons()
    {


        if (name_d.Length == group_d.Length && name_d.Length == value_d.Length)
        {
            print("flag1");
            for (int i = 0; i < name_d.Length; i++)
            {
                print("flag2");

                GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
                button.name = name_d[i];
                button.GetComponentInChildren<TextMeshProUGUI>().text = name_d[i];

                /*// Cambia el color del botón aquí
                Color newButtonColor = Color.red; // Puedes definir el color que prefieras
                button.GetComponent<Image>().color = newButtonColor;*/

                Color buttonColor = GetColorByIndex(i);
                button.GetComponent<Image>().color = buttonColor;





                int tempo = 0, control = 0;

                ObjetoCaracteristicas caracteristicas = button.GetComponent<ObjetoCaracteristicas>();
                if (caracteristicas != null)
                {

                    print("flag3");
                    int.TryParse(value_d[i], out tempo);
                    int.TryParse(control_d[i], out control);

                    caracteristicas.nombreObjeto = name_d[i];
                    caracteristicas.nombreGrupo = group_d[i];
                    caracteristicas.Valor_objet = tempo.ToString();
                    caracteristicas.valor2 = control.ToString();
                }
            }
        }
        else
        {
            Debug.LogError("Los arrays no tienen la misma longitud.");
        }

        Color GetColorByIndex(int index)
        {
            // Define una lista de colores
            Color[] colors = new Color[]
            {

            Naranja,/*
            Color.blue,
            Color.green,
            Color.yellow,
            Color.cyan,*/
               
            };
            return colors[index % colors.Length];
        }

    }// generador boton

    //ControlBoton
    void asigna()
    {

        // Inicializa el arreglo con el número de hijos que son botones
        arregloBotones = new Button[objetoPadre.transform.childCount];

        // Asigna cada hijo al arreglo
        for (int i = 0; i < objetoPadre.transform.childCount; i++)
        {
            // Asume que cada hijo tiene un componente Button
            arregloBotones[i] = objetoPadre.transform.GetChild(i).GetComponent<Button>();
        }
    }//Asigna End


    //butunOrder
    public Button ObtenerBotonPorIndice(int indice)
    {
        if (indice >= 0 && indice < arregloBotones.Length)
        {
            return arregloBotones[indice];
        }
        else
        {
            Debug.LogError("Índice fuera de rango.");
            return null;
        }
    }
 
    public void OrganizeGrid()
    {
        // Crear o actualizar prefabs para cada grupo único
        foreach (Transform child in parentObject)
        {
            ObjetoCaracteristicas caracteristicas = child.GetComponent<ObjetoCaracteristicas>();
            if (caracteristicas != null)
            {
                string groupName = caracteristicas.nombreGrupo;
                if (!groupObjects.ContainsKey(groupName))
                {
                    // Instanciar el prefab en lugar de crear un nuevo GameObject
                    GameObject groupObject = Instantiate(groupPrefab, parentObject);
                    groupObject.name = groupName; // Asignar el nombre del grupo al prefab instanciado
                    groupObject.SetActive(false); // Desactivar el objeto de grupo
                    groupObjects.Add(groupName, groupObject);
                }
            }
        }

        // Mover cada botón al objeto de grupo correspondiente
        foreach (Transform child in parentObject)
        {
            ObjetoCaracteristicas caracteristicas = child.GetComponent<ObjetoCaracteristicas>();
            if (caracteristicas != null)
            {
                string groupName = caracteristicas.nombreGrupo;
                if (groupObjects.ContainsKey(groupName))
                {
                    child.SetParent(groupObjects[groupName].transform, false);
                }
            }
        }

        // Organizar los botones dentro de cada grupo
        foreach (KeyValuePair<string, GameObject> groupPair in groupObjects)
        {
            RectTransform groupRectTransform = groupPair.Value.GetComponent<RectTransform>();
            groupRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            groupRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            groupRectTransform.pivot = new Vector2(0.5f, 0.5f);

            Button[] buttons = groupPair.Value.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform buttonRectTransform = buttons[i].GetComponent<RectTransform>();
                int row = i / columns;
                int column = i % columns;
                float xPosition = column * (buttonSize.x + spacing);
                float yPosition = -row * (buttonSize.y + spacing);
                buttonRectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
                buttonRectTransform.sizeDelta = buttonSize;
            }
        }
    }

    void GenerateCategoryButtons()
    {
     
       

        // Generar un botón para cada grupo
        int index = 0;
        foreach (var group in groupObjects)
        {
            // Crear un nuevo botón y configurarlo
            GameObject button = Instantiate(buttonPrefabFood, buttonParentFood);
            button.name = "Button_" + group.Key;

            // Asignar el texto de la categoría utilizando TextMeshPro
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>(true);
            if (buttonText != null)
            {
                buttonText.text = group.Key;
            }
            else
            {
                Debug.LogError("No se encontró el componente TMP_Text en el prefab del botón.");
            }

            // Asignar el evento OnClick
            Button btn = button.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() => ToggleGroup(group.Value));
            }
            else
            {
                Debug.LogError("No se encontró el componente Button en el prefab del botón.");
            }

            // Ajustar el tamaño y la posición del botón
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.sizeDelta = buttonSize;
            int row = index / buttonColumns;
            int column = index % buttonColumns;
            float xPosition = column * (buttonSize.x + buttonSpacing) - (buttonColumns / 2.0f * buttonSize.x) + (buttonSize.x / 2.0f);
            float yPosition = -row * (buttonSize.y + buttonSpacing);
            rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

            index++;
        }
    }

    void ToggleGroup(GameObject group)
    {
        // Activar o desactivar el grupo correspondiente
        group.SetActive(!group.activeSelf);
    }



    // Update is called once per frame
    void Update()
    {
 
        if (timer > 3f && paso)
        {
            paso = false;
            ///implementar delta time, para el cargado de elementos
            GenerateButtons();
            
            print("generar botones unpdate activado");

        }
        if (timer > 5f)
        {
            paso = false;
            ///implementar delta time, para el cargado de elementos
            OrganizeGrid();

            print("organizador activado");

        }
        if (timer > 6f)
        {
            paso = false;
            ///implementar delta time, para el cargado de elementos
            if (!buttonsGenerated)
            {
                GenerateCategoryButtons();
                buttonsGenerated = true; // Evitar la generación múltiple de botones
            }

            print("generador de botones categoria activado");

        }
       

        if (oneShot && objetoPadre != null)
        {
            asigna();
            oneShot = false;
        }



        if (timer > 20f)
        {
            FillArrays();
            timer = 0;
            print("refresco el lector");
        }

        timer = timer + Time.deltaTime;

    }//update

  
  
}
