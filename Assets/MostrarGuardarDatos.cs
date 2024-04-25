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
        rutaArchivo = Application.dataPath + "/SaveData/datosObjeto.txt";
    }

    // Función para mostrar la información en el TMP Text
  

    // Función para guardar la información en un archivo
    public void GuardarInformacionEnArchivo()
    {
        // Se crea un objeto StreamWriter para escribir en el archivo
        using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
        {
            // Se escribe la información en el archivo
            writer.WriteLine(textoRecibido);
            writer.Close();

            // Se muestra un mensaje de confirmación
            Debug.Log("Información guardada en el archivo: " + rutaArchivo);
        }
    }

    // Función para asociar la función GuardarInformacionEnArchivo a un botón
    public void AsociarFuncionABoton(Button boton)
    {
        // Se agrega un listener al botón para llamar a la función GuardarInformacionEnArchivo cuando se haga clic
        boton.onClick.AddListener(GuardarInformacionEnArchivo);
    }

    public void Update()
    {
        blockText = blockText + textoRecibido;
        textoAMostrar.text = "\n " + blockText;
    }
}
