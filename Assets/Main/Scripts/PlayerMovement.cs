using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 9f;
    public float jumpForce = 6f;
    public LayerMask groundMask;

    // Sprinting
    public KeyCode sprintKey = KeyCode.LeftShift;
  
    private float sprintSpeedMultiplier = 1.5f;
  
    private float sprintEnergyCost = 15f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isSprinting;

    // Hormones
    // TODO:
    // - DOPAMINE
    // - CORTISOL
    // - ENDORPHINS
    // - TESTOSTERONE
    // - OXYTOCIN

    private float maxDopamineLevel = 100f; // General: Speed V, Jump V, Strength TODO. If you reach max dopamine, you die V.
    private float maxCortisolLevel = 100f; // Health
    private float maxEndorphinLevel = 100f; // Defense
    private float maxAdrenalineLevel = 100f; // Sprint
    private float maxTestosteroneLevel = 100f; // Strength
    private float maxOxytocinLevel = 100f; // Cortisol reduction
  
    public float adrenalineLevel = 0;
    public float dopamineLevel = 0; 
    public float testosteroneLevel = 0;
    public float endorphinLevel = 0;
    public float cortisolLevel = 0;
    public float oxytocinLevel = 0;
  
    private float hormoneDecayRate = 1f;
    private float dopamineSpeedMultiplier = 0.02f;
    private float dopamineJumpMultiplier = 0.01f;
  
    private float startingDopamineLevel = 50f;

    public DeathScreen deathScreen;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        adrenalineLevel = maxAdrenalineLevel;
        dopamineLevel = startingDopamineLevel;
    }

    void Update()
    {
       if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpForce + (dopamineLevel * dopamineJumpMultiplier)), ForceMode.Impulse);
        }

        if(Input.GetKey(sprintKey) && adrenalineLevel > 0)
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

        float speed = moveSpeed + (dopamineLevel * dopamineSpeedMultiplier);
        if(isSprinting)
        {
            speed *= sprintSpeedMultiplier;
            adrenalineLevel -= sprintEnergyCost * Time.fixedDeltaTime;
            adrenalineLevel = Mathf.Max(0, adrenalineLevel);
        }

        Vector3 move = (transform.forward * v + transform.right * h) * speed;
        Vector3 newVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        rb.linearVelocity = newVelocity;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);

        if (dopamineLevel >= maxDopamineLevel)
        {
            Death();
        }

        if (cortisolLevel >= maxCortisolLevel)
        {
            Death();
        }

        // Hormone decay
        dopamineLevel = Mathf.MoveTowards(dopamineLevel, startingDopamineLevel, hormoneDecayRate * Time.fixedDeltaTime);
        cortisolLevel = Mathf.Max(0, cortisolLevel - hormoneDecayRate * Time.fixedDeltaTime);
        endorphinLevel = Mathf.Max(0, endorphinLevel - hormoneDecayRate * Time.fixedDeltaTime);
        adrenalineLevel = Mathf.Max(0, adrenalineLevel - hormoneDecayRate * Time.fixedDeltaTime);
        testosteroneLevel = Mathf.Max(0, testosteroneLevel - hormoneDecayRate * Time.fixedDeltaTime);
        oxytocinLevel = Mathf.Max(0, oxytocinLevel - hormoneDecayRate * Time.fixedDeltaTime);

        Debug.Log("Dopamine: " + dopamineLevel + " | Cortisol: " + cortisolLevel + " | Endorphins: " + endorphinLevel + " | Adrenaline: " + adrenalineLevel + " | Testosterone: " + testosteroneLevel + " | Oxytocin: " + oxytocinLevel);
    }

    private void Death()
    {
        moveSpeed = 0;
        jumpForce = 0;
        deathScreen.TriggerDeath();
    }
}
