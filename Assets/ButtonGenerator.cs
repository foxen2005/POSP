using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting; // Asegúrate de incluir este namespace para TextMeshPro

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab del botón
    public GameObject buttonParent; // GameObject padre de los botones
    public string[] buttonNames, group_name, buttonValue; // Arreglo de nombres de botones
    //public int[] buttonValue;
    bool paso = true;
    
    

    void Start()
    {
        buttonNames = SpreadsheetLoader.name_d_cop;
        group_name = SpreadsheetLoader.group_d_cop;
        buttonValue = SpreadsheetLoader.value_d_cop;

       // GenerateButtons();
    }
   
    void GenerateButtons()
    {
    
        // Itera sobre cada nombre en el arreglo y crea un botón
        foreach (string name in buttonNames)
        {
           
            GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
            button.name = name;
            button.GetComponentInChildren<TextMeshProUGUI>().text = name; ObjetoCaracteristicas caracteristicas = buttonPrefab.GetComponent<ObjetoCaracteristicas>();

        }
        /*
          foreach (string name in buttonNames)
          {

             control++;
              
              caracteristicas.ConfigurarObjeto(buttonNames[control], group_name[control], int.Parse(buttonValue[control]));

              GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
              button.name = name;
              // Accede al componente TextMeshProUGUI para cambiar el texto
              button.GetComponentInChildren<TextMeshProUGUI>().text = name;
          }*/


    }

    /*
     * 
     *    void GenerateButtons()
    {
        // Itera sobre cada nombre en el arreglo y crea un botón
        foreach (string name in buttonNames)
        {
            GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
            button.name = name;
            // Accede al componente TextMeshProUGUI para cambiar el texto
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
    }

   /*public class UniqueValueAssigner : MonoBehaviour
    {
        public int uniqueValue; // Este es el valor único que se asignará al objeto

        // Puedes llamar a este método para instanciar el objeto y asignarle un valor único
        public static GameObject InstantiateWithUniqueValue(GameObject prefab, int value, Vector3 position, Quaternion rotation)
        {
            GameObject instance = Instantiate(prefab, position, rotation);
            UniqueValueAssigner assigner = instance.AddComponent<UniqueValueAssigner>();
            assigner.uniqueValue = value;
            return instance;
        }
    }*/

    void Update()
    {

        if (SpreadsheetLoader.name_d_cop != null && paso == true)
        {

            buttonNames = SpreadsheetLoader.name_d_cop;
            GenerateButtons();
            paso = false;
            
        }
       

    }

}
