using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con los botones de la UI

public class ControlBoton : MonoBehaviour
{
    public GameObject objetoPadre; // Asigna el objeto padre en el inspector
    public Button[] arregloBotones; // Arreglo para almacenar los botones
    public bool oneShot = true;

    void Start()
    {
        // Aseg�rate de que el objeto padre est� asignado
        
    }

    void Update()
    {
        if (oneShot && objetoPadre != null) {

            asigna();
            oneShot = false;


        }
    }

    void asigna()
    {

            // Inicializa el arreglo con el n�mero de hijos que son botones
            arregloBotones = new Button[objetoPadre.transform.childCount];

            // Asigna cada hijo al arreglo
            for (int i = 0; i < objetoPadre.transform.childCount; i++)
            {
                // Asume que cada hijo tiene un componente Button
                arregloBotones[i] = objetoPadre.transform.GetChild(i).GetComponent<Button>();
            }
      

    }

    // M�todo para acceder a un bot�n espec�fico por �ndice
    public Button ObtenerBotonPorIndice(int indice)
    {
        if (indice >= 0 && indice < arregloBotones.Length)
        {
            return arregloBotones[indice];
        }
        else
        {
            Debug.LogError("�ndice fuera de rango.");
            return null;
        }
    }

   
}
