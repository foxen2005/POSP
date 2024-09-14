using UnityEngine;
using UnityEngine.UI;

public class GridObjectInstantiator : MonoBehaviour
{
    public GameObject uiPrefabToInstantiate; // Asigna el prefab del UI en el Inspector
    public Transform parentTransform; // Asigna el objeto padre en el Inspector

    private void Start()
    {
        InstantiateUIObjects();
    }

    private void InstantiateUIObjects()
    {
        if (uiPrefabToInstantiate == null || parentTransform == null)
        {
            Debug.LogWarning("Asigna el prefab del UI y el objeto padre en el Inspector.");
            return;
        }

        // Verificar si hay PlayerPrefs ocupados
        if (PlayerPrefs.HasKey("OccupiedSlot"))
        {
            // Instancia el prefab del UI
            Instantiate(uiPrefabToInstantiate, parentTransform);
            Debug.Log("Se ha instanciado un objeto ocupado.");
        }
        else
        {
            Debug.Log("No hay objetos ocupados para instanciar.");
        }
    }
}
