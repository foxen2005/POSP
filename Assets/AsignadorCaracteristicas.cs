using UnityEngine;

public class AsignadorCaracteristicas : MonoBehaviour
{
    // Variables globales para asignar a los hijos
    public string nombreGlobalObjeto;
    public string nombreGlobalGrupo;
    public int valorGlobalObjeto;
    public int valorGlobal2;

    void Start()
    {
        // Asegúrate de que hay al menos un hijo
        if (transform.childCount > 0)
        {
            // Encuentra el primer hijo
            Transform primerHijo = transform.GetChild(0);

            // Busca el script 'ObjetoCaracteristica' en el primer hijo
            ObjetoCaracteristicas caracteristicas = primerHijo.GetComponent<ObjetoCaracteristicas>();

            // Si el script existe en el hijo, asigna las variables globales
            if (caracteristicas != null)
            {
                caracteristicas.nombreObjeto = nombreGlobalObjeto;
                caracteristicas.nombreGrupo = nombreGlobalGrupo;
                caracteristicas.Valor_objet = valorGlobalObjeto;
                caracteristicas.valor2 = valorGlobal2;
            }
        }
    }
}
