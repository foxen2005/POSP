using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting; // Asegúrate de incluir este namespace para TextMeshPro

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab del botón
    public GameObject buttonParent; // GameObject padre de los botones
    public string[] buttonNames, group_name, buttonValue, controlValue; // Arreglo de nombres de botones
    //public int[] buttonValue;
    bool paso = true;
    
    

    void Start()
    {
        
        
        // GenerateButtons();
    }
   
    void GenerateButtons()
    {

      /*  //Itera sobre cada nombre en el arreglo y crea un botón
        foreach (string name in buttonNames)
        {
           
            GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
            ObjetoCaracteristicas caracteristicas = buttonPrefab.GetComponent<ObjetoCaracteristicas>();

            button.name = name;
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
            
       
        }*/
        if (buttonNames.Length == group_name.Length && buttonNames.Length == buttonValue.Length)
        {
            for (int i = 0; i < buttonNames.Length; i++)
            {
                
                GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
                button.name = buttonNames[i];
                button.GetComponentInChildren<TextMeshProUGUI>().text = buttonNames[i];

                int tempo = 0, control = 0;

                ObjetoCaracteristicas caracteristicas = button.GetComponent<ObjetoCaracteristicas>();
                if (caracteristicas != null)
                {


                    int.TryParse(buttonValue[i], out tempo);
                    int.TryParse(controlValue[i], out control);

                    caracteristicas.nombreObjeto = buttonNames[i];
                    caracteristicas.nombreGrupo = group_name[i];
                    caracteristicas.Valor_objet = tempo.ToString();
                    caracteristicas.valor2 = control.ToString();
                }
            }
        }
        else
        {
            Debug.LogError("Los arrays no tienen la misma longitud.");
        }

    }

  

    void Update()
    {

        if (SpreadsheetLoader.name_d_cop != null && paso == true)
        {
            controlValue = SpreadsheetLoader.control_d_cop;
            group_name = SpreadsheetLoader.group_d_cop;
            buttonValue = SpreadsheetLoader.value_d_cop;

            buttonNames = SpreadsheetLoader.name_d_cop;
            GenerateButtons();
            paso = false;
            
        }
       

    }

}
