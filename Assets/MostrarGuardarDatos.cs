using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;

public class MostrarGuardarDatos : MonoBehaviour
{
    // Variables para el texto
    public TMP_Text textoAMostrar, textNotificaEscritura, RutaText; // Texto donde se mostrará la información
    public static string textoRecibido; // Cadena que contiene la información del objeto
    string blockText, sheet = "sheet = \"sheet = \\\"google.visualization.Query.setResponse({ \\\"\\\"version\\\"\\\":\\\"\\\"0.6\\\"\\\",\\\"\\\"reqId\\\"\\\":\\\"\\\"0\\\"\\\",\\\"\\\"status\\\":\\\"\\\"\\\"\\\"\\\"\\\"ok\\\"\\\",\\\"\\\"sig\\\"\\\":\\\"\\\"1299033392\\\"\\\",\\\"\\\"table\\\"\\\":{ \\\"\\\"cols\\\"\\\":[{ \\\"\\\"id\\\":\\\"A\\\"\\\",\\\"\\\"label\\\":\\\"\\\"\\\",\\\"\\\"type\\\":\\\"string\\\"},{ \\\"id\\\":\\\"B\\\",\\\"label\\\":\\\"\\\",\\\"type\\\":\\\"string\\\"},{ \\\"id\\\":\\\"C\\\",\\\"label\\\":\\\"\\\",\\\"type\\\":\\\"string\\\"},{ \\\"id\\\":\\\"D\\\",\\\"label\\\":\\\"\\\",\\\"type\\\":\\\"string\\\"}],\\\"rows\\\":[{ \\\"c\\\":[{ \\\"v\\\":\\\"name\\\"},{ \\\"v\\\":\\\"value\\\"},{ \\\"v\\\":\\\"group\\\"},{ \\\"v\\\":\\\"control\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Pollo\\\"},{ \\\"v\\\":\\\"0000\\\"},{ \\\"v\\\":\\\"Comida\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Pizza\\\"},{ \\\"v\\\":\\\"0000\\\"},{ \\\"v\\\":\\\"Comida\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Pasta\\\"},{ \\\"v\\\":\\\"3500\\\"},{ \\\"v\\\":\\\"Comida\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Cocacola\\\"},{ \\\"v\\\":\\\"2002\\\"},{ \\\"v\\\":\\\"Bebidas\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Fanta\\\"},{ \\\"v\\\":\\\"1500\\\"},{ \\\"v\\\":\\\"Bebidas\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Agua\\\"},{ \\\"v\\\":\\\"1111\\\"},{ \\\"v\\\":\\\"Bebidas\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Galletas\\\"},{ \\\"v\\\":\\\"2111\\\"},{ \\\"v\\\":\\\"Postres\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Chicles\\\"},{ \\\"v\\\":\\\"1550\\\"},{ \\\"v\\\":\\\"Postres\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Torta\\\"},{ \\\"v\\\":\\\"1500\\\"},{ \\\"v\\\":\\\"Postres\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"pastelito\\\"},{ \\\"v\\\":\\\"2500\\\"},{ \\\"v\\\":\\\"fritura\\\"},{ \\\"v\\\":\\\"0\\\"}]},{ \\\"c\\\":[{ \\\"v\\\":\\\"Camarones\\\"},{ \\\"v\\\":\\\"11110\\\"},{ \\\"v\\\":\\\"Comida\\\"},{ \\\"v\\\":\\\"0\\\"}]}],\\\"parsedNumHeaders\\\":0} })\\\" ;\";";
    string[] TextArray;









    public SpreadsheetWriter FuncionCall;
    public PrintPDF impresionPdf;
    public PrintingManager GeneradorPDF;

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


            RutaText.text = rutaArchivoActual;
            // Muestra un mensaje de confirmación
            Debug.Log("Información guardada en el archivo: " + rutaArchivoActual);
            Debug.Log("Información guardada en el archivo: " + blockText);


            //PDF smartPDF 
           /* impresionPdf = GameObject.Find("Main Camera").GetComponent<PrintPDF>();
            impresionPdf.PrintDocument(blockText);*/

            //printingManagerPDF
           GeneradorPDF = GameObject.Find("Main Camera").GetComponent<PrintingManager>();
           
            GeneradorPDF.GenerateFile(blockText, contadorArchivo.ToString());
           // GeneradorPDF.PrintFiles(contadorArchivo.ToString());

          /*  //enviar a server
            FuncionCall = GameObject.Find("Main Camera").GetComponent<SpreadsheetWriter>();
            FuncionCall.EnviarDatos(sheet);*/


        }

        blockText = "";
        textoRecibido = "";
        ControladorSuma.SUMA = 0;



        if (File.Exists(rutaArchivoActual))
        {
            textNotificaEscritura.text = "Archivo creado con éxito.";
        }
        else
        {
            textNotificaEscritura.text = "Error: El archivo no se pudo crear.";
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