using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PanelSizeAdjuster : MonoBehaviour, IDragHandler
{
    public float padding = 10f; // Espacio adicional para ajustar el tamaño del panel
    public Vector2 manualSize = new Vector2(100f, 100f); // Tamaño manual que se puede ajustar desde el inspector
    public Vector3 scale = new Vector3(1f, 1f, 1f); // Escala que se puede ajustar desde el inspector

    public int childCount; // Variable para contar los hijos activos
    public float totalWidth; // Suma del ancho de todos los hijos
    public float totalHeight; // Altura promedio de los hijos (suma de alturas / 3)

    public RectTransform rectTransform;
    private Canvas canvas;
    private bool adjustSizeAutomatically = false; // Bandera para activar el ajuste automático

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("PanelSizeAdjuster: No se encontró un Canvas en los padres.");
        }
    }

    void Update()
    {
        // Contar solo los hijos activos
        int activeChildCount = 0;
        float totalChildWidth = 0f;
        float totalChildHeight = 0f;

        foreach (RectTransform child in rectTransform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                activeChildCount++;
                totalChildWidth += child.rect.width;
                totalChildHeight += child.rect.height;
            }
        }

        // Asignar los valores a las variables públicas
        childCount = activeChildCount;
        totalWidth = totalChildWidth;
        totalHeight = childCount == 1 ? totalChildHeight : totalChildHeight / 3f;

        // Ajustar el tamaño del panel
        rectTransform.sizeDelta = new Vector2(totalWidth + (2 * padding), totalHeight + (2 * padding));
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
}
