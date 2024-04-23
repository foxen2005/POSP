using UnityEngine;

public class DraggableCharacter : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private Vector2 initialMousePosition;
    private Vector3 initialObjectPosition;

    void Start()
    {
        cam = Camera.main; // Asegúrate de que la cámara principal esté etiquetada como "MainCamera"
    }

    void Update()
    {
        // Detecta si el jugador está tocando uno de los personajes
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                initialMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                initialObjectPosition = transform.position;
            }
        }

        if (isDragging)
        {
            Vector2 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 positionDelta = currentMousePosition - initialMousePosition;
            transform.position = initialObjectPosition + new Vector3(positionDelta.x, positionDelta.y, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
