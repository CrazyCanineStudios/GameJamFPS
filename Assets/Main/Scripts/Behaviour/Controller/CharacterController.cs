using System;
using UnityEngine;

/// <summary>
/// Character Controller handles jumping, movement and ground checks.
/// </summary>
public class CharacterController : MonoBehaviour
{
    public CharacterMaster Master { get; private set; }
    public Rigidbody Body { get; private set; }

    private Collider capsuleCollider;

    [Header("Ground Movement")]
    [SerializeField] private float maxSpeed = 10;

    private float horizontal, vertical;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 6f;
    private Vector3 jumpVelocity;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheckParent = null;
    private Transform[] groundCheckLocations;
    public bool isGrounded;

    private Transform mainCam;

    public bool isMoving;

    private Vector3 moveDirection;

    // Input
    private bool jumpPressed;
    private float inputAmount;
    public float floorOffsetY;
    public Vector3 velocity;

    private void Awake()
    {
        Master = GetComponent<CharacterMaster>();

        // Create a new array for ground checks using the ground check parent's children.
        groundCheckLocations = new Transform[groundCheckParent.childCount];

        for (int i = 0; i < groundCheckLocations.Length; i++)
        {
            // Set the ground check location to the parent's child.
            groundCheckLocations[i] = groundCheckParent.GetChild(i);
        }
        Body = this.GetComponent<Rigidbody>();
        capsuleCollider = this.GetComponent<CapsuleCollider>();

        // Set the jump Velocity.
        jumpVelocity = new Vector3(0, jumpHeight, 0);

        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        if (Master.CanMove == false) { Body.velocity = Vector3.zero; return; }

        // Check for Input.
        CheckForInput();

        // Stop the rigidbody from sleeping if the character doesn't move which disables collision detection.
        Body.WakeUp();

        // Check for jump.
        CheckForJump();

        // Get the Player's Input.
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 correctedVertical = vertical * mainCam.transform.forward;
        Vector3 correctedHorizontal = horizontal * mainCam.transform.right;

        Vector3 combinedInput = correctedHorizontal + correctedVertical;

        moveDirection = combinedInput.normalized;
        moveDirection.y = 0;

        // Ensure input is not negative or above one.
        float inputMagnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        inputAmount = Mathf.Clamp01(inputMagnitude);

        // Rotate to Camera.
        transform.rotation = Quaternion.Lerp(transform.rotation, mainCam.rotation, 3f);
    }

    private void FixedUpdate()
    {
        if (Master.CanMove == false) { Body.velocity = Vector3.zero; return; }

        isGrounded = CheckIfGrounded();

        // Actual Movement
        velocity = moveDirection * maxSpeed * inputAmount;

        Body.velocity = new Vector3(velocity.x, Body.velocity.y, velocity.z);
    }

    /// <summary>
    /// Checks for player input such as moving, jumping etc.
    /// </summary>
    private void CheckForInput()
    {
        jumpPressed = Input.GetButtonDown("Jump") && isGrounded;

        isMoving = horizontal + vertical != 0;
    }

    /// <summary>
    /// Check if there is ground below the character and determine if they are grounded or not.
    /// </summary>
    private bool CheckIfGrounded()
    {
        // Get the distance to the ground from the center of the collider.
        float groundDistance = capsuleCollider.bounds.extents.y;
        foreach (Transform check in groundCheckLocations)
        {
            RaycastHit hit;
            if (Physics.Raycast(check.position, Vector3.down, out hit, groundDistance + 0.05f))
            {
                // Found the ground.
                if (!hit.transform.GetComponent<Collider>().isTrigger)
                {
                    return true;
                }
            }
        }
        // Did not find the ground.
        return false;
    }

    /// <summary>
    /// Check if the jump button has been pressed and jump if it has.
    /// </summary>
    private void CheckForJump()
    {
        if (jumpPressed)
        {
            Jump(jumpVelocity);
        }
    }

    /// <summary>
    /// Make the character jump using the jumpVelocity Vector3. A positive Y value will push the character upwards.
    /// </summary>
    /// <param name="jumpVelocity"></param>
    private void Jump(Vector3 jumpVelocity)
    {
        // Add the jumpVelocity force to the character's rigidbody.
        Body.AddRelativeForce(jumpVelocity, ForceMode.Impulse);
    }
}
