using UnityEngine;

public class ObjetoCaracteristicas : MonoBehaviour
{
    public string nombreObjeto, nombreGrupo;
    public int Valor_objet;
    public int valor2;

    // Inicializa el objeto con sus caracter�sticas
    public void ConfigurarObjeto(string nombre, string Grou, int Value)
    {
        nombreObjeto = nombre;
        nombreGrupo = Grou;
        Valor_objet = Value;
    }

    // M�todo para mostrar las caracter�sticas en la consola
    public void MostrarCaracteristicas()
    {
        Debug.Log("Nombre: " + nombreObjeto + ", Valor 1: " + nombreGrupo + ", Valor 2: " + Valor_objet);
    }
}
