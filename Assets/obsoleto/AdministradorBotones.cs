using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI

public class AdministradorBotones : MonoBehaviour
{
    public GameObject objetoPadre; // Asigna el objeto padre en el inspector
    public Button[] arregloBotones; // Arreglo para almacenar los botones

    void Start()
    {
        // Aseg�rate de que el objeto padre est� asignado
        if (objetoPadre != null)
        {
            // Inicializa el arreglo con el n�mero de hijos que son botones
            arregloBotones = new Button[objetoPadre.transform.childCount];

            // Asigna cada hijo que es un bot�n al arreglo
            for (int i = 0; i < objetoPadre.transform.childCount; i++)
            {
                // Asume que cada hijo tiene un componente Button
                arregloBotones[i] = objetoPadre.transform.GetChild(i).GetComponent<Button>();
                // Aseg�rate de agregar un listener para cada bot�n
                if (arregloBotones[i] != null)
                {
                    arregloBotones[i].onClick.AddListener(() => BotonPresionado(arregloBotones[i]));
                }
            }
        }
    }

    void BotonPresionado(Button boton)
    {
        // Imprime el nombre del bot�n presionado en la consola
        Debug.Log("Bot�n presionado: " + boton.gameObject.name);
    }
}
