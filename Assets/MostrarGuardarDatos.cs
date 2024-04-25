using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;

public class MostrarGuardarDatos : MonoBehaviour
{
    // Variables para el texto
    public TMP_Text textoAMostrar; // Texto donde se mostrar� la informaci�n
    public static string textoRecibido; // Cadena que contiene la informaci�n del objeto
    string blockText;

    // Variable para el archivo
    private string rutaArchivo; // Ruta del archivo donde se guardar� la informaci�n

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
            // Escribe la informaci�n en el archivo
            writer.WriteLine(blockText);
            writer.Close();

            // Muestra un mensaje de confirmaci�n
            Debug.Log("Informaci�n guardada en el archivo: " + rutaArchivoActual);
            Debug.Log("Informaci�n guardada en el archivo: " + blockText);
        }

        blockText = "";
        textoRecibido = "";



    }


    // Funci�n para asociar la funci�n GuardarInformacionEnArchivo a un bot�n
    public void AsociarFuncionABoton(Button boton)
    {
        // Se agrega un listener al bot�n para llamar a la funci�n GuardarInformacionEnArchivo cuando se haga clic
        boton.onClick.AddListener(GuardarInformacionEnArchivo);
    }

    public void Update()
    {
       // blockText = blockText + "\n"+textoRecibido;
        textoAMostrar.text = textoRecibido;
    }
}