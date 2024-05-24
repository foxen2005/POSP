using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PanelSizeAdjuster : MonoBehaviour, IDragHandler
{
    public float padding = 10f; // Espacio adicional para ajustar el tamaño del panel
    public Vector2 manualSize = new Vector2(100f, 100f); // Tamaño manual que se puede ajustar desde el inspector
    public Vector3 scale = new Vector3(1f, 1f, 1f); // Escala que se puede ajustar desde el inspector

    private RectTransform rectTransform;
    private Canvas canvas;

    void Awake()
    {
        

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
       
        if (canvas == null)
        {
            Debug.LogError("PanelSizeAdjuster: No se encontró un Canvas en los padres.");
        }
        SetAnchorToTopLeft();
       // CenterPanelOnScreen();

    }

    void Start()
    {
        AdjustSizeToChildren();
        
    }
   


private void SetAnchorToTopLeft()
{
    // Establecer el anclaje en la esquina superior izquierda
    rectTransform.anchorMin = new Vector2(0, 1);
    rectTransform.anchorMax = new Vector2(0, 1);

    // Posicionar el objeto en la esquina superior izquierda
    rectTransform.anchoredPosition = new Vector2(0, 0);
}

private void CenterPanelOnScreen()
    {
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void AdjustSizeToChildren()
    {
        // Tamaño fijo de los hijos
        float childWidth = 80f;
        float childHeight = 80f;

        // Contar solo los hijos activos
        int activeChildCount = 0;
        foreach (RectTransform child in rectTransform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                activeChildCount++;
            }
        }

        // Calcular el número de filas necesarias
        int rows = Mathf.CeilToInt(activeChildCount / 3f);

        // Calcular el ancho y la altura total del panel
        float panelWidth = (3 * childWidth) + (2 * padding); // 3 hijos por fila + padding entre columnas
        float panelHeight = (rows * childHeight) + ((rows - 1) * padding) + (2 * padding); // Altura total de las filas + padding entre filas + padding superior e inferior

        // Ajustar el tamaño del panel
        rectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
    }

    public void SetManualSize()
    {
        rectTransform.sizeDelta = manualSize; // Aplicar el tamaño manual al panel
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Función para escalar el panel
    public void ScalePanel()
    {
        rectTransform.localScale = scale;
    }


    public void Update()
    {
        SetManualSize(); // Establecer el tamaño manual al inicio
        ScalePanel(); // Aplicar la escala al inicio
    }
}
