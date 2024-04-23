using UnityEngine;
using SimpleJSON; // Asegúrate de incluir SimpleJSON

public class JsonProcessor : MonoBehaviour
{


    float timer, timerStop;

    // Define los arreglos para almacenar los datos
    public string[] name_d, value_d, group_d, control_d;
    public static string[] name_d_cop, value_d_cop, group_d_cop, control_d_cop;

    // El JSON como string
    private string jsonString;

 


    void Start()
    {
        timerStop = 5f;
        if (jsonString == null)
        {
            print("vacio");
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
    }
    void Update()
    {

        if (timer > timerStop)
        {
            jsonString = SpreadsheetLoader.jsonContent;
            ProcessJson(jsonString);
            timer = 0;
            print("refresco el procesador");


            name_d_cop = name_d;
            value_d_cop = value_d;
            group_d_cop = group_d;
            control_d_cop = control_d;

            timerStop = 30f;
        }
        timer = timer + Time.deltaTime;


        if (jsonString == null)
        {
            print("vacio");
        }
    }
}


