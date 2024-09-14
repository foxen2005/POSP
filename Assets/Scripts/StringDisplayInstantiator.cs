using UnityEngine;
using TMPro;

public class StringDisplayInstantiator : MonoBehaviour
{
    public GameObject textPrefab; // Prefab con componente TextMeshPro
    public Transform parentTransform; // Transform del objeto padre para los textos
    public StringStorage stringStorage; // Referencia al script StringStorage

    // Llamar a esta función desde otro script para instanciar un objeto de texto
    public void InstantiateAllTextObjects()
    {
        for (int i = 0; i < stringStorage.currentSlotIndex; i++)
        {
            string displayString = stringStorage.LoadStringFromPlayerPrefs(i);
            CreateTextObject(i, displayString);
        }
    }

    private void CreateTextObject(int index, string displayString)
    {
        // Instanciar el prefab y configurar el texto
        GameObject newTextObject = Instantiate(textPrefab, parentTransform);
        TextMeshPro textComponent = newTextObject.GetComponent<TextMeshPro>();
        if (textComponent != null)
        {
            textComponent.text = $"[{index}] {displayString}";
        }
        else
        {
            Debug.LogError("El prefab no tiene un componente TextMeshPro.");
        }
    }
}
