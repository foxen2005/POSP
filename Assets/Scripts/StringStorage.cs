using UnityEngine;

public class StringStorage : MonoBehaviour
{
    private string slotPrefix = "Slot"; // Prefijo para las claves en PlayerPrefs
    private int maxSlots = 10; // Número inicial de casillas
    public int currentSlotIndex = 0; // Índice actual para almacenar el siguiente string

    // Función para guardar un string en PlayerPrefs
    public void SaveStringToPlayerPrefs(string inputString)
    {
        // Generar la clave para la casilla actual
        string currentSlotKey = $"{slotPrefix}{currentSlotIndex:000}";

        // Verificar si la casilla actual está ocupada
        while (PlayerPrefs.HasKey(currentSlotKey))
        {
            currentSlotIndex++; // Pasar al siguiente índice
            currentSlotKey = $"{slotPrefix}{currentSlotIndex:000}";
        }

        // Guardar el string en la casilla actual
        PlayerPrefs.SetString(currentSlotKey, inputString);
        Debug.Log($"Guardado en {currentSlotKey}: {inputString}");

        // Incrementar el índice para la siguiente casilla
        currentSlotIndex++;

        // Ajustar el número máximo de casillas si es necesario
        if (currentSlotIndex >= maxSlots)
        {
            maxSlots++; // Incrementar el número máximo
            Debug.Log($"Número máximo de casillas aumentado a {maxSlots}");
            Debug.Log($"Número máximo de casillas aumentado a {currentSlotIndex}");
        }
    }

    // Función para leer un string desde PlayerPrefs
    public string LoadStringFromPlayerPrefs(int slotIndex)
    {
        string currentSlotKey = $"{slotPrefix}{slotIndex:000}";
        return PlayerPrefs.GetString(currentSlotKey, "Valor predeterminado");
    }

    // Ejemplo de uso
    private void Start()
    {
        // Llamar a la función con un string de ejemplo
       // SaveStringToPlayerPrefs("Hola, mundo!");

        // Leer el valor almacenado en la primera casilla (índice 0)
        string valorLeido = LoadStringFromPlayerPrefs(0);
        Debug.Log($"Valor leído: {valorLeido}");
    }
}
