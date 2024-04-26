using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpreadsheetWriter : MonoBehaviour
{
    // URL de tu aplicaci�n web de Google Apps Script
    string googleAppsScriptUrl = "https://script.google.com/macros/s/AKfycbztMzbdnOBL7mzHp3jLM_Q7KowB5EF8mfcIfAcA-eZRy2qg64X38nPlINiHM_Iu_R1sBg/exec";

    // M�todo para enviar datos al Google Sheet
    public void EnviarDatos(string datos)
    {
        print(datos);
        StartCoroutine(EnviarRequest(datos));
    }

    IEnumerator EnviarRequest(string datos)
    {
        WWWForm form = new WWWForm();
        // Agrega los datos al formulario. Aseg�rate de que coincidan con la estructura esperada por tu script.
        for (int i = 0; i < datos.Length; i++)
        {
            form.AddField("dato" + i, datos[i].ToString());
        }

        UnityWebRequest request = UnityWebRequest.Post(googleAppsScriptUrl, form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Datos enviados al Google Sheet correctamente.");
        }
    }
}
