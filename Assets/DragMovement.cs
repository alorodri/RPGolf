using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour
{
    public float forceMultiplier = 10f;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;
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
            rb.AddForce(force);
        } else if (Input.GetMouseButtonUp(1))
        {
            float distance = Vector3.Distance(worldPos, transform.position);
            Vector3 force = (worldPos - transform.position).normalized * distance * forceMultiplier;
            rb.AddForce(-force);
        }
    }
}
