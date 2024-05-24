using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class ReadWriteFile : MonoBehaviour
{
    // Ruta del archivo CSV
    public string csvFilePath = "Assets/Resources/POSP_data.csv";

    // Arreglos para almacenar los datos como strings
    public string[] names;
    public string[] values;
    public string[] groups;
    public string[] controls;

    void Start()
    {
        // Llenamos los arreglos con los datos del archivo CSV
        FillArrays();
    }

    private void FillArrays()
    {
        // Verificamos que el archivo CSV exista
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("El archivo CSV no se encuentra en la ruta especificada: " + csvFilePath);
            return;
        }

        // Leemos todas las l�neas del archivo CSV
        string[] lines = File.ReadAllLines(csvFilePath);

        // Inicializamos los arreglos basados en el n�mero de l�neas (excluyendo la l�nea de encabezados)
        names = new string[lines.Length - 1];
        values = new string[lines.Length - 1];
        groups = new string[lines.Length - 1];
        controls = new string[lines.Length - 1];

        // Procesamos cada l�nea (excluyendo la l�nea de encabezados)
        for (int i = 1; i < lines.Length; i++)
        {
            // Separamos los valores por punto y coma
            string[] valuesInLine = lines[i].Split(';');

            // Verificamos que haya suficientes valores en la l�nea
            if (valuesInLine.Length < 4)
            {
                Debug.LogError("La l�nea " + i + " no tiene suficientes valores.");
                continue;
            }

            // Asignamos los valores a los arreglos correspondientes
            names[i - 1] = valuesInLine[0].Trim();
            values[i - 1] = valuesInLine[1].Trim();
            groups[i - 1] = valuesInLine[2].Trim();
            controls[i - 1] = valuesInLine[3].Trim();
        }
    }

    // Ejemplo de c�mo llamar a la funci�n para actualizar los datos
    public void UpdateData()
    {
        FillArrays();
        // Actualizar la interfaz o l�gica del juego con los datos actualizados
    }
}
