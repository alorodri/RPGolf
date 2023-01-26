using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    public float pullForce = 10f;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ball"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector2 holePos = new Vector2(transform.position.x, transform.position.y);
            Vector2 force = (holePos - rb.position).normalized * pullForce;
            rb.AddForce(force);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
