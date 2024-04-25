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
        rutaArchivo = Application.dataPath + "/SaveData/datosObjeto.txt";
    }

    // Funci�n para mostrar la informaci�n en el TMP Text
  

    // Funci�n para guardar la informaci�n en un archivo
    public void GuardarInformacionEnArchivo()
    {
        // Se crea un objeto StreamWriter para escribir en el archivo
        using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
        {
            // Se escribe la informaci�n en el archivo
            writer.WriteLine(textoRecibido);
            writer.Close();

            // Se muestra un mensaje de confirmaci�n
            Debug.Log("Informaci�n guardada en el archivo: " + rutaArchivo);
        }
    }

    // Funci�n para asociar la funci�n GuardarInformacionEnArchivo a un bot�n
    public void AsociarFuncionABoton(Button boton)
    {
        // Se agrega un listener al bot�n para llamar a la funci�n GuardarInformacionEnArchivo cuando se haga clic
        boton.onClick.AddListener(GuardarInformacionEnArchivo);
    }

    public void Update()
    {
        blockText = blockText + textoRecibido;
        textoAMostrar.text = "\n " + blockText;
    }
}
