using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;

public class GridOrganizer : MonoBehaviour
{
    public RectTransform gridArea; // El área del RectTransform donde se organizarán los botones
    public int columns = 3; // Número de columnas de la grilla
    public float spacing = 5f; // Espaciado entre los botones
    public Vector2 buttonSize = new Vector2(100f, 50f); // Tamaño de los botones (ancho, alto)
    public float timer;

    // Diccionario para almacenar los grupos y sus objetos correspondientes
    private Dictionary<string, GameObject> groupObjects = new Dictionary<string, GameObject>();

    void Start()
    {
       
    }

    public void OrganizeGrid()
    {
        // Crear objetos vacíos para cada grupo único
        foreach (Transform child in gridArea)
        {
            ObjetoCaracteristicas caracteristicas = child.GetComponent<ObjetoCaracteristicas>();
            if (caracteristicas != null)
            {
                string groupName = caracteristicas.nombreGrupo;
                if (!groupObjects.ContainsKey(groupName))
                {
                    GameObject groupObject = new GameObject(groupName);
                    groupObject.transform.SetParent(gridArea, false);
                    groupObjects.Add(groupName, groupObject);
                }
            }
        }

        // Mover cada botón al objeto de grupo correspondiente
        foreach (Transform child in gridArea)
        {
            ObjetoCaracteristicas caracteristicas = child.GetComponent<ObjetoCaracteristicas>();
            if (caracteristicas != null)
            {
                string groupName = caracteristicas.nombreGrupo;
                if (groupObjects.ContainsKey(groupName))
                {
                    child.SetParent(groupObjects[groupName].transform, false);
                }
            }
        }

        // Organizar los botones dentro de cada grupo
        foreach (KeyValuePair<string, GameObject> groupPair in groupObjects)
        {
            RectTransform groupRectTransform = groupPair.Value.AddComponent<RectTransform>();
            groupRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            groupRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            groupRectTransform.pivot = new Vector2(0.5f, 0.5f);

            Button[] buttons = groupPair.Value.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform buttonRectTransform = buttons[i].GetComponent<RectTransform>();
                int row = i / columns;
                int column = i % columns;
                float xPosition = column * (buttonSize.x + spacing);
                float yPosition = -row * (buttonSize.y + spacing);
                buttonRectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
                buttonRectTransform.sizeDelta = buttonSize;
            }
        }
    }

    private void Update()
    {
        if (timer > 10f)
        {
            OrganizeGrid();
                timer = 0;

        }


            timer = timer + Time.deltaTime;
    }
}
