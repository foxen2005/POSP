using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;

public class MostrarGuardarDatos : MonoBehaviour
{
    // Variables para el texto
    public TMP_Text textoAMostrar; // Texto donde se mostrará la información
    public static string textoRecibido; // Cadena que contiene la información del objeto
    string blockText;

    // Variable para el archivo
    private string rutaArchivo; // Ruta del archivo donde se guardará la información

    // Start
    void Start()
    {
        // Se establece la ruta del archivo
     //   rutaArchivo = Application.dataPath + "/SaveData/datosObjeto.txt";
    }





    public void GuardarInformacionEnArchivo()
    {
        blockText = textoRecibido;

        // Ruta base del archivo
        string rutaBaseArchivo = Application.dataPath + "/SaveData/datosObjeto";

        // Contador para generar nombres de archivo secuenciales
        int contadorArchivo = 1;

        // Ruta del archivo actual
        string rutaArchivoActual = rutaBaseArchivo + contadorArchivo + ".txt";

        // Verifica si el archivo existe
        while (File.Exists(rutaArchivoActual))
        {
            // Incrementa el contador
            contadorArchivo++;

            // Genera una nueva ruta de archivo
            rutaArchivoActual = rutaBaseArchivo + contadorArchivo + ".txt";
        }

        // Abre el archivo para escribir
        using (StreamWriter writer = new StreamWriter(rutaArchivoActual, true))
        {
            // Escribe la información en el archivo
            writer.WriteLine(blockText);
            writer.Close();

            // Muestra un mensaje de confirmación
            Debug.Log("Información guardada en el archivo: " + rutaArchivoActual);
            Debug.Log("Información guardada en el archivo: " + blockText);
        }

        blockText = "";
        textoRecibido = "";



    }


    // Función para asociar la función GuardarInformacionEnArchivo a un botón
    public void AsociarFuncionABoton(Button boton)
    {
        // Se agrega un listener al botón para llamar a la función GuardarInformacionEnArchivo cuando se haga clic
        boton.onClick.AddListener(GuardarInformacionEnArchivo);
    }

    public void Update()
    {
       // blockText = blockText + "\n"+textoRecibido;
        textoAMostrar.text = textoRecibido;
    }
}