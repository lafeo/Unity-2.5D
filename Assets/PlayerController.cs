using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator; // Reference to the Animator

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>(); // Get the Animator
    }

    // Update is called once per frame
    void Update()
    {
        // Check for ground detection
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        // Get horizontal and vertical input axes
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDir = new Vector3(-x, 0, y);

        // Update velocity based on movement direction and speed
        rb.velocity = moveDir * speed;

        // Flip sprite based on direction
        if (x != 0 && x > 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x < 0)
        {
            sr.flipX = false;
        }

        // Debug: Output velocity magnitude
        float velocityMagnitude = rb.velocity.magnitude;
        Debug.Log("Velocity Magnitude: " + velocityMagnitude);  // Check if this value changes when you move

        // Set the Speed parameter in Animator
        animator.SetFloat("Speed", velocityMagnitude);  
        // This should trigger the correct animation
    }
}
