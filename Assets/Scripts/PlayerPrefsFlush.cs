using UnityEngine;

public class PlayerPrefsFlush : MonoBehaviour
{
    public bool flushOnEnable = false; // Variable pública para activar el flush al habilitar el objeto

    private void OnEnable()
    {
        if (flushOnEnable)
        {
            FlushPlayerPrefs();
        }
    }

    public void FlushPlayerPrefs()
    {
        // Borrar todas las claves almacenadas en PlayerPrefs
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs se ha vaciado correctamente.");
    }
}
