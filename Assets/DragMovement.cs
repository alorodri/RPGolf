using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour
{
    public float forceMultiplier = 10f;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;
    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;

        // paint line from ball to mouse
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, worldPos);

        if (Input.GetMouseButtonUp(0))
        {
            float distance = Vector3.Distance(worldPos, transform.position);
            Vector3 force = (worldPos - transform.position).normalized * distance * forceMultiplier;
            lastPosition = transform.position;
            rb.AddForce(force);
        } else if (Input.GetMouseButtonUp(1))
        {
            float distance = Vector3.Distance(worldPos, transform.position);
            Vector3 force = (worldPos - transform.position).normalized * distance * forceMultiplier;
            lastPosition = transform.position;
            rb.AddForce(-force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Comprueba si el objeto con el que colisiona la pelota tiene el material WaterMaterial
        if (collider.gameObject.GetComponent<Renderer>().sharedMaterial.name == "WaterMaterial")
        {
            // Aquí puedes escribir el código que desees ejecutar cuando la pelota choca con el cubo con el material WaterMaterial.
            // Por ejemplo, volver a la posición desde donde se golpeó
            Vector3 startingPosition = new Vector3(0, 0, 0);
            transform.position = lastPosition;
            rb.velocity = new Vector2(0, 0);
        }
    }
}
