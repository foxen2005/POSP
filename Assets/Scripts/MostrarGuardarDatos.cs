using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;
using System;

public class MostrarGuardarDatos : MonoBehaviour
{
    // Variables para el texto
    public TMP_Text textoAMostrar, textNotificaEscritura, RutaText; // Texto donde se mostrar� la informaci�n
    public static string textoRecibido; // Cadena que contiene la informaci�n del objeto
    string blockText;
    string[] TextArray;
    public string tempo;
    private StringStorage GuardadorString;



    private PrintingManager GeneradorPDF;

    // Variable para el archivo
    private string rutaArchivo; // Ruta del archivo donde se guardar� la informaci�n

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
            // Escribe la informaci�n en el archivo
            writer.WriteLine("Pedido: " + contadorArchivo + "\n\n" + blockText);
            writer.Close();


           // RutaText.text = rutaArchivoActual;
            // Muestra un mensaje de confirmaci�n
            Debug.Log("Informaci�n guardada en el archivo: " + rutaArchivoActual);
            Debug.Log("Informaci�n guardada en el archivo: " + blockText);


            

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
            Debug.Log("Archivo creado con �xito.");
            //textNotificaEscritura.text = "Archivo creado con �xito.";
        }
        else
        {
            Debug.Log("Error: El archivo no se pudo crear.");
            //textNotificaEscritura.text = "Error: El archivo no se pudo crear.";
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
       // blockText = blockText + "\n"+textoRecibido;
        textoAMostrar.text = textoRecibido;
    }
}