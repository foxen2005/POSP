using UnityEngine;
using UnityEngine.UI;

public class ObjetoCaracteristicas : MonoBehaviour
{
    public string nombreObjeto, nombreGrupo;
    public int Valor_objet;
    public int valor2;

    public int valorASumar; // Valor a sumar a SUMA cada vez que se presione el botón

    // Referencia al botón, asigna esto en el inspector de Unity
    private Button miBoton;


 void Start()
    {
        // Obtiene el componente Button del objeto actual
        miBoton = GetComponent<Button>();
        if (miBoton != null)
        {
            // Agrega un listener al botón que llamará al método SumarValor cuando se haga clic
            miBoton.onClick.AddListener(SumarValor);
        }
    }



    // Inicializa el objeto con sus caracter�sticas
    public void ConfigurarObjeto(string nombre, string Grou, int Value, int control)
    {
        nombreObjeto = nombre;
        nombreGrupo = Grou;
        Valor_objet = Value;
        valor2 = control;
    }

    // M�todo para mostrar las caracter�sticas en la consola
    public void MostrarCaracteristicas()
    {
        Debug.Log("Nombre: " + nombreObjeto + ", Valor 1: " + nombreGrupo + ", Valor 2: " + Valor_objet);
    }

    void SumarValor()
    {
        // Suma valorASumar a la variable estática SUMA del script ControladorSuma
        ControladorSuma.SUMA += valorASumar;
        Debug.Log("Nuevo valor de SUMA: " + ControladorSuma.SUMA);
    }
}


}
