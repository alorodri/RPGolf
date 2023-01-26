using UnityEngine;
using UnityEngine.Events;

public class BallHoleDetection : MonoBehaviour
{
    public UnityEvent ballInHole;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ball"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb.velocity.magnitude < 20f)
            {
                ballInHole?.Invoke();
            }
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
