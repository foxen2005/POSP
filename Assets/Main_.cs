using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using Unity.VisualScripting;


public class Main_ : MonoBehaviour
{

    //Carga Json
    public string spreadsheetId = "1Ft2m1xzDQl-P3QXbM6uIbeknBa4reGfDNppSyhZFsjY";
    public string gid = "0";
    public static string jsonContent;
    float timer;
    float timerStop;
    public string[] name_d, value_d, group_d, control_d;
    

    private string jsonString;

    //BotonGenerador
    public GameObject buttonPrefab; // Prefab del botón
    public GameObject buttonParent; // GameObject padre de los botones
   public bool paso = false;

    //ControlBoton
    public GameObject objetoPadre; // Asigna el objeto padre en el inspector
    public Button[] arregloBotones; // Arreglo para almacenar los botones
    public bool oneShot = true;




    void Start()
    {

                StartCoroutine(LoadSpreadsheet()); //Json Carga
               
    }

    //Json
    IEnumerator LoadSpreadsheet()
    {
        string url = "https://docs.google.com/spreadsheets/d/" + spreadsheetId + "/gviz/tq?tqx=out:json&tq&gid=" + gid;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Procesa los datos JSON aquí
            string data = request.downloadHandler.text;
            jsonContent = data.Substring(47).TrimEnd(')', ';');
            ProcessJson(jsonContent);
        }
    }

    void ProcessJson(string json)
    {
        // Elimina los caracteres adicionales al principio y al final del JSON
        string data = json.Substring(json.IndexOf('{'), json.LastIndexOf('}') - json.IndexOf('{') + 1);

        // Parsea el JSON
        var parsedData = JSON.Parse(data);

        // Extrae las filas del JSON
        var rows = parsedData["table"]["rows"];

        // Inicializa listas temporales para almacenar los datos
        var idList = new System.Collections.Generic.List<string>();
        var nameList = new System.Collections.Generic.List<string>();
        var roleList = new System.Collections.Generic.List<string>();
        var nickList = new System.Collections.Generic.List<string>();

        // Itera a través de las filas y extrae los datos
        for (int i = 0; i < rows.Count; i++)
        {
            idList.Add(rows[i]["c"][0]["v"]);
            nameList.Add(rows[i]["c"][1]["v"]);
            roleList.Add(rows[i]["c"][2]["v"]);
            nickList.Add(rows[i]["c"][3]["v"]);
        }

        // Convierte las listas en arreglos
        name_d = idList.ToArray();
        value_d = nameList.ToArray();
        group_d = roleList.ToArray();
        control_d = nickList.ToArray();

    

    }//jason

    //Json end

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

                int tempo = 0, control = 0;

                ObjetoCaracteristicas caracteristicas = button.GetComponent<ObjetoCaracteristicas>();
                if (caracteristicas != null)
                {

                    print("flag3");
                    int.TryParse(value_d[i], out tempo);
                    int.TryParse(control_d[i], out control);

                    caracteristicas.nombreObjeto = name_d[i];
                    caracteristicas.nombreGrupo = group_d[i];
                    caracteristicas.Valor_objet = tempo;
                    caracteristicas.valor2 = control;
                }
            }
        }
        else
        {
            Debug.LogError("Los arrays no tienen la misma longitud.");
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
    }//buton order ends





    //Boton generador End

    // Update is called once per frame
    void Update()
    {
        //JSON update

        if (timer > 150f)
        {
            StartCoroutine(LoadSpreadsheet());
            timer = 0;
            print("refresco el lector");
        }
        timer = timer + Time.deltaTime;

        //json endd

        //BotonGenerador

        if (paso)
        {
                     ///implementar delta time, para el cargado de elementos
            GenerateButtons();
            paso = false;
            print("generar botones unpdate activado");

        }
        //botongenerator Ends
        if (oneShot && objetoPadre != null)
        {
            asigna();
            oneShot = false;
        }

       


    }//update

  
  
}
