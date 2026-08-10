using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private Transform camTransform;

    private Rigidbody rb;
    private Vector3 moveInput;

    private void Awake()
    {
        //Get rigid body component
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        //Keyboard input
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
            {
                moveZ += 1f;
            }
            if (Keyboard.current.sKey.isPressed)
            {
                moveZ -= 1f;
            }
            if (Keyboard.current.dKey.isPressed)
            {
                moveX += 1f;
            }
            if (Keyboard.current.aKey.isPressed)
            {
                moveX -= 1f;
            }
            if (Keyboard.current.spaceKey.isPressed)
            {
                Jump();
            }
        }

        //Controller input
        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            if (stick.magnitude > 0.1f)
            {
                moveX = stick.x;
                moveZ = stick.y;
            }
            if (Gamepad.current.buttonSouth.isPressed)
            {
                Jump();
            }
        }

        //Movement conversion based on camera rotation

            //Get forawrd and rights directions of the camera
            Vector3 camForward = camTransform.forward;
            Vector3 camRight = camTransform.right;

            //Set the y of the vectors to 0 to avoid flight
            camForward.y = 0f;
            camRight.y = 0f;

            //Normalize vectors
            camForward.Normalize();
            camRight.Normalize();

            //Combine input and camera direction
            moveInput = (camForward * moveZ + camRight * moveX).normalized;

            //Rotate character to movement direction
            if (moveInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveInput);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
            }
        
    }

    private void FixedUpdate()
    {
        //Apply speed
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 targetVelocity = moveInput * moveSpeed;

        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, moveInput.z * moveSpeed);
    }

    private bool IsGrounded()
    {
       
        return Physics.CheckSphere (groundCheck.position, 0.5f, groundLayer);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSpeed, rb.linearVelocity.z);
        }
    }
}
