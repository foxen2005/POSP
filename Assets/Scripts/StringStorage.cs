using UnityEngine;

public class StringStorage : MonoBehaviour
{
    private string slotPrefix = "Slot"; // Prefijo para las claves en PlayerPrefs
    private int maxSlots = 10; // N�mero inicial de casillas
    public int currentSlotIndex = 0; // �ndice actual para almacenar el siguiente string

    // Funci�n para guardar un string en PlayerPrefs
    public void SaveStringToPlayerPrefs(string inputString)
    {
        // Generar la clave para la casilla actual
        string currentSlotKey = $"{slotPrefix}{currentSlotIndex:000}";

        // Verificar si la casilla actual est� ocupada
        while (PlayerPrefs.HasKey(currentSlotKey))
        {
            currentSlotIndex++; // Pasar al siguiente �ndice
            currentSlotKey = $"{slotPrefix}{currentSlotIndex:000}";
        }

        // Guardar el string en la casilla actual
        PlayerPrefs.SetString(currentSlotKey, inputString);
        Debug.Log($"Guardado en {currentSlotKey}: {inputString}");

        // Incrementar el �ndice para la siguiente casilla
        currentSlotIndex++;

        // Ajustar el n�mero m�ximo de casillas si es necesario
        if (currentSlotIndex >= maxSlots)
        {
            maxSlots++; // Incrementar el n�mero m�ximo
            Debug.Log($"N�mero m�ximo de casillas aumentado a {maxSlots}");
            Debug.Log($"N�mero m�ximo de casillas aumentado a {currentSlotIndex}");
        }
    }

    // Funci�n para leer un string desde PlayerPrefs
    public string LoadStringFromPlayerPrefs(int slotIndex)
    {
        string currentSlotKey = $"{slotPrefix}{slotIndex:000}";
        return PlayerPrefs.GetString(currentSlotKey, "Valor predeterminado");
    }

    // Ejemplo de uso
    private void Start()
    {
        // Llamar a la funci�n con un string de ejemplo
       // SaveStringToPlayerPrefs("Hola, mundo!");

        // Leer el valor almacenado en la primera casilla (�ndice 0)
        string valorLeido = LoadStringFromPlayerPrefs(0);
        Debug.Log($"Valor le�do: {valorLeido}");
    }
}
