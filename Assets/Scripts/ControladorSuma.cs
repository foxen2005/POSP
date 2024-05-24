using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorSuma : MonoBehaviour
{
    public static int SUMA; // Variable estática pública inicializada en 0

    public TextMeshProUGUI Text_Value_total;

    // Start is called before the first frame update
    void Start()
    {
        SUMA = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Text_Value_total.text = "$ " + SUMA.ToString();
        
    }

  
        
    

}
