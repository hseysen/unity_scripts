using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonPlayerController : MonoBehaviour
{
    [SerializeField] private Transform lookCamera = null;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private bool hideCursor = true;
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float gravity = -13.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float slopeForce = 5.0f;
    [SerializeField] private float slopeForceRayLength = 2.0f;
    [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField] private KeyCode jumpKey;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    bool isJumping = false;
    CharacterController controller = null;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVel = Vector2.zero;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVel = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        // Take mouse input and orient the camera accordingly
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVel, mouseSmoothTime);
        transform.Rotate(Vector3.up * mouseSensitivity * currentMouseDelta.x);

        cameraPitch -= currentMouseDelta.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        lookCamera.localEulerAngles = Vector3.right * cameraPitch;
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVel, moveSmoothTime);

        // Handle gravity
        if (controller.isGrounded) PlayerLanded();
        velocityY += gravity * Time.deltaTime;

        // Handle jumping
        JumpInput();

        // Calculate the velocity and move
        Vector3 vel = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(vel * Time.deltaTime);

        // If the player is on a slope, gravity should be increased in order to avoid the jittery falling effect
        if(currentDir != Vector2.zero && PlayerOnSlope()) controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
    }

    bool PlayerOnSlope()
    {
        // If the player is jumping, it means they are not on a slope
        if (isJumping) return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up) return true;
        return false;
    }

    void JumpInput()
    {
        // Only allow the player to jump when they are on the ground
        if (Input.GetKeyDown(jumpKey) && controller.isGrounded)
        {
            isJumping = true;
            velocityY = Mathf.Sqrt(-2.0f * jumpHeight * gravity);
        }

        // When the player hits the ceiling, stop jumping
        if (isJumping && controller.collisionFlags == CollisionFlags.Above) PlayerLanded();
    }

    void PlayerLanded()
    {
        // Stop jumping
        isJumping = false;
        velocityY = 0.0f;
    }
}
