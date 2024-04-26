using System.IO;
using TMPro;
using UnityEngine;

public class ContadorDeArchivos : MonoBehaviour
{
    // Ruta de la carpeta a buscar
    private string rutaCarpeta = "/idbfs/";

    // Variable pública de TextMeshProUGUI para mostrar el número de archivos
    public TextMeshProUGUI textoDeArchivos;

    void Start()
    {
        
    }

    void ContarArchivosEnCarpeta()
    {
        // Verifica si la carpeta existe
        if (Directory.Exists(rutaCarpeta))
        {
            // Obtiene todos los archivos en la carpeta
            string[] archivos = Directory.GetFiles(rutaCarpeta);
            // Muestra la cantidad de archivos en la variable TextMeshProUGUI
            textoDeArchivos.text = "Hay " + archivos.Length + " archivos en la carpeta.";
            Debug.Log("Hay " + archivos.Length + " archivos en la carpeta " + rutaCarpeta);
        }
        else
        {
            textoDeArchivos.text = "La carpeta no existe.";
            Debug.Log("La carpeta no existe: " + rutaCarpeta);
        }
    }

    private void Update()
    {
        ContarArchivosEnCarpeta();
    }
}
