using UnityEngine;
using UnityEngine.UI;

public class GridOrganizer : MonoBehaviour
{
    public RectTransform gridArea; // El área del RectTransform donde se organizarán los botones
    public int columns = 3; // Número de columnas de la grilla
    public float spacing = 5f; // Espaciado entre los botones
    public Vector2 buttonSize = new Vector2(100f, 50f); // Tamaño de los botones (ancho, alto)

    void Update()
    {
        // Verifica si hay hijos antes de organizar
        if (gridArea.childCount > 0)
        {
            OrganizeGrid();
        }
    }

    void OrganizeGrid()
    {
        int childCount = gridArea.childCount;
        float width = gridArea.rect.width;
        float xStartPosition = -width / 2 + buttonSize.x / 2;
        float yStartPosition = gridArea.rect.height / 2 - buttonSize.y / 2  - 45;
        int row = 0;
        int column = 0;

        for (int i = 0; i < childCount; i++)
        {
            if (i % columns == 0 && i != 0)
            {
                row++;
                column = 0;
            }

            float xPosition = xStartPosition + column * (buttonSize.x + spacing);
            float yPosition = yStartPosition - row * (buttonSize.y + spacing);

            RectTransform child = gridArea.GetChild(i) as RectTransform;
            child.anchoredPosition = new Vector2(xPosition, yPosition);
            child.sizeDelta = buttonSize;

            column++;
        }
    }
}
