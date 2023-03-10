using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DragMovement : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public float triggerStopBallVelocity = 0.3f;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;
    private Vector3 lastPosition;
    private bool holeFilled = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BallHoleDetection bhd = GameObject.FindWithTag("hole").GetComponent<BallHoleDetection>();
        bhd.ballInHole.AddListener(BallInHole);
        InitializeLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;

        // paint line from ball to mouse

        if (rb.velocity.magnitude < 0.003f && !holeFilled) {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, Vector3.Lerp(transform.position, worldPos, 0.5f));
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
        } else
        {
            lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        StopBallWhenVelocityNearZero();
        Vector2 movementDirection = rb.velocity.normalized;
        float distanceNextFrame = rb.velocity.magnitude * Time.fixedDeltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, distanceNextFrame);
        Debug.DrawRay(transform.position, movementDirection);
        if (hit.collider != null && hit.collider.isTrigger)
        {
            OnTriggerEnter2D(hit.collider); // revisar como se comporta esto cuando tenga rampas y una pared o agua al final y cosas as?
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var colliderRenderer = collider.gameObject.GetComponent<Renderer>();
        if (colliderRenderer == null) return;
        string materialName = colliderRenderer.sharedMaterial.name;
        // Comprueba si el objeto con el que colisiona la pelota tiene el material WaterMaterial
        switch (materialName)
        {
            case "WaterMaterial":
                // Si choca contra el agua, vuelve a la ?ltima posici?n y se frena
                transform.position = lastPosition;
                rb.velocity = new Vector2(0, 0);
                break;
            case "LavaMaterial":
                // En este caso, volveremos al punto de partida del mapa
                break;
        }
    }

    private void InitializeLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.generateLightingData = true;
        lineRenderer.material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/LineRendererMaterial.mat");
    }

    private void StopBallWhenVelocityNearZero()
    {
        if (rb.velocity.magnitude < triggerStopBallVelocity)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void BallInHole()
    {
        lineRenderer.enabled = false;
        holeFilled = true;
        rb.velocity = Vector3.zero;
        iTween.ScaleTo(rb.gameObject, Vector3.zero, 1f);
    }
}
