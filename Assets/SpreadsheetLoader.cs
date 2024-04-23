using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;

public class SpreadsheetLoader : MonoBehaviour
{
    public string spreadsheetId = "1Ft2m1xzDQl-P3QXbM6uIbeknBa4reGfDNppSyhZFsjY";
    public string gid = "0";
    public static string jsonContent;
    float timer;


    float  timerStop;
    public string[] name_d, value_d, group_d, control_d;
    public static string[] name_d_cop, value_d_cop, group_d_cop, control_d_cop;

    // El JSON como string
    private string jsonString;



    // Inicia la carga de la hoja de cálculo al iniciar
    void Start()
    {
        StartCoroutine(LoadSpreadsheet());
    }

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

        name_d_cop = name_d;
        value_d_cop = value_d;
        group_d_cop = group_d;
        control_d_cop = control_d;

    }

    void Update()
    {
        if (timer > 150f)
        {
           
            StartCoroutine(LoadSpreadsheet());
            timer = 0;
            print("refresco el lector");
        
        }
        timer = timer + Time.deltaTime;
        
    }
}
