using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 5f;
    public LayerMask groundMask;
    public float MaxEnergy = 100f;

    // Sprinting
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeedMultiplier = 1.5f;
    public float sprintEnergyCost = 15f;

    private Rigidbody rb;
    private bool isGrounded;
    private float currentEnergy;
    private bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentEnergy = MaxEnergy;
    }

    void Update()
    {
       if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if(Input.GetKey(sprintKey) && currentEnergy > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float speed = moveSpeed;
        if(isSprinting)
        {
            speed *= sprintSpeedMultiplier;
            currentEnergy -= sprintEnergyCost * Time.fixedDeltaTime;
            currentEnergy = Mathf.Max(0, currentEnergy);
        }

        Vector3 move = (transform.forward * v + transform.right * h) * speed;
        Vector3 newVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        rb.linearVelocity = newVelocity;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
    }
}

// Movement abilities ??:
// - Wall Jump
