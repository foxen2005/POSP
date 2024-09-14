using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;
using System;

public class MostrarGuardarDatos : MonoBehaviour
{
    // Variables para el texto
    public TMP_Text textoAMostrar, textNotificaEscritura, RutaText; // Texto donde se mostrará la información
    public static string textoRecibido; // Cadena que contiene la información del objeto
    string blockText;
    string[] TextArray;
    public string tempo;
    private StringStorage GuardadorString;



    private PrintingManager GeneradorPDF;

    // Variable para el archivo
    private string rutaArchivo; // Ruta del archivo donde se guardará la información

    // Start
    void Start()
    {

  
    }







    public void GuardarInformacionEnArchivo()
    {
        blockText = textoRecibido +  "\nTotal: $ " + ControladorSuma.SUMA + "\n";

        // Ruta base del archivo
       //string rutaBaseArchivo = Application.dataPath + "/SaveData/datosObjeto";
       string rutaBaseArchivo = Application.persistentDataPath ;
       

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
            writer.WriteLine("Pedido: " + contadorArchivo + "\n\n" + blockText);
            writer.Close();


           // RutaText.text = rutaArchivoActual;
            // Muestra un mensaje de confirmación
            Debug.Log("Información guardada en el archivo: " + rutaArchivoActual);
            Debug.Log("Información guardada en el archivo: " + blockText);


            

            //printingManagerPDF
           GeneradorPDF = GameObject.Find("Main Camera").GetComponent<PrintingManager>();
           
            GeneradorPDF.GenerateFile(blockText, contadorArchivo.ToString());
           // GeneradorPDF.PrintFiles(contadorArchivo.ToString());

       


        }

        tempo = blockText;

        GuardadorString = GameObject.Find("Main Camera").GetComponent<StringStorage>();

        GuardadorString.SaveStringToPlayerPrefs(tempo);


        blockText = "";
        textoRecibido = "";
        ControladorSuma.SUMA = 0;
        tempo = "";



        if (File.Exists(rutaArchivoActual))
        {
            Debug.Log("Archivo creado con éxito.");
            //textNotificaEscritura.text = "Archivo creado con éxito.";
        }
        else
        {
            Debug.Log("Error: El archivo no se pudo crear.");
            //textNotificaEscritura.text = "Error: El archivo no se pudo crear.";
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
       // blockText = blockText + "\n"+textoRecibido;
        textoAMostrar.text = textoRecibido;
    }
}