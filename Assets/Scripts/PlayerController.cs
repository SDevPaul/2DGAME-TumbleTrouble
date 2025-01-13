using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Accessible in Unity Inspector
    [SerializeField] public int gems = 0;
    [SerializeField] private TextMeshProUGUI gemsCount;
    
    // Accessible in Unity Inspector and other scripts
    public float speed; // Player Speed    
    public float jumpForce; // Jump force
    public float jumpCooldown; // Cooldown time between jumps

    Vector2 checkpointPos;  // Stores current respawn point
    private float lastJumpTime; // Time of the last jump
    private bool isJumping; // To check if the player is jumping
    private Rigidbody2D rb; // Players RigidBody
    private float knockback = 15f; // Knockback when the player gets hit by the Enemy
    AudioManager audioManager;
    DoorMechanics DoorMechanics;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        DoorMechanics = GameObject.FindGameObjectWithTag("Door").GetComponent<DoorMechanics>();
    }


    void Start()
    {
        // Stores all necessary values into the respective variables
        checkpointPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 3.8f;  // Adjust the effect of gravity on this object
        rb.angularDrag = 0.6f;  // Resistance to rotational movement
        rb.drag = 1.6f;
        speed = 9f;
        jumpCooldown = 0.7f;
        jumpForce = 12.5f;


    }

    // Update is called once per frame
    void Update()
    {
        // Player Physics that is ball like
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(Vector2.right * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(-Vector2.right * speed);
        }

        // Player can only Jump at a certain period and when groud is touched
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastJumpTime + jumpCooldown && !isJumping)
        {
            isJumping = true;
            if (isJumping)
            {
                Jump();
                audioManager.PlaySFX(audioManager.Jumped);

            }
        }

        // Return to Checkpoint Callout
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            audioManager.PlaySFX(audioManager.RespawnSFX);
            updatePosition();
            
        }

        

    }

    // Jump Function
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
        lastJumpTime = Time.time; // Record the time of the jump
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        // When Ground is touched, the player can jump again
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        // Conditon to let Enemy knock off the Player
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioManager.PlaySFX(audioManager.Bat);
            // If the Player touches the enemy from the right, the player gets knock to the left
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-knockback, rb.velocity.y); 
            }

            // else to the right
            else
            {
                rb.velocity = new Vector2(knockback, rb.velocity.y);
            }
        }
    }

    // Function that Updates the Player RespawnPoint
    void updatePosition()
    {
        // Updates the player checkpoint
        transform.position = checkpointPos;

        // Prevents player moving from previous forces when respawning
        rb.velocity = Vector2.zero;
        FreezeRotationTemporarily(0.5f);
    }

    // Function that Freezes the Player rotation temporarily
    private void FreezeRotationTemporarily(float duration)
    {
        // Freeze the ball's rotation
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Call UnfreezeRotation after the specified duration
        Invoke("UnfreezeRotation", duration);
    }

    // Function that lets the Player rotate again
    private void UnfreezeRotation()
    {
        // Remove the rotation constraints, allowing the ball to rotate again
        rb.constraints = RigidbodyConstraints2D.None;
    }

    // Function that updates the checkpointpos variable to the checkpoint location 
    public void updateCheckpointPos(Vector2 pos)
    {
        checkpointPos = pos;
    }

    // Function that lets Player collect gems
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Gem is touched
        if (collision.CompareTag("Gems"))
        {

            // Destroy the Gem
            Destroy(collision.gameObject);

            // Update the gems collected of the player
            audioManager.PlaySFX(audioManager.pickUpGem);
            gems += 1;

            // Display the amount of gem the Player collected to the UI
            gemsCount.text = gems.ToString();

            // When door is interactable, a sound will indicate
            if (DoorMechanics.gemsNeeded == gems)
            {
                audioManager.PlaySFX(audioManager.DoorOpened);
            }
            


        }
    }
}
