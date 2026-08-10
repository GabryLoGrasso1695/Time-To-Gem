using UnityEngine;
using UnityEngine.InputSystem;

public class CamerFollow : MonoBehaviour
{
    //Target and offset
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private float rotationSpeed = 50f;

    //Set a max and a minimum for pitch
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 70f;

    //Camera collisions
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float cameraRadius = 0.2f;
    [SerializeField] private float minDistance = 0.5f;

    private float currentYaw = 0f;
    private float currentPitch = 15f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //Camera rotation
    private void Update()
    {
        //Show again mouse cursor
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Mouse input
        if (Keyboard.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            currentYaw += mouseDelta.x * rotationSpeed * Time.deltaTime;
            currentPitch += mouseDelta.y * rotationSpeed * Time.deltaTime;

        }

        //Controller stick input
        if (Gamepad.current != null)
        {
            Vector2 rightStick = Gamepad.current.rightStick.ReadValue();

            currentYaw += rightStick.x * rotationSpeed * Time.deltaTime;
            currentPitch += rightStick.y * rotationSpeed * Time.deltaTime;

        }
        
        //Limit camera pitch
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
    }
    private void LateUpdate()
    {
        if (target == null) return;


        //Set pivot for camera rotation
        Vector3 pivotPosition = target.position + Vector3.up * 1.0f;

        //Calculate rotation and offset for camera
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f); //Convert inclination angles in a quarernion
        Vector3 desiredOffset = rotation * offset; //Multiply quaternion by the offset to rotate it

        //Springarm collisions management through sphere cast
        Vector3 rayDirection = desiredOffset.normalized;
        float maxDistance = desiredOffset.magnitude;
        float currentDistance = maxDistance;

        //Generate a Sphere with cameraRadius as its radius from pivot position, following rayDirection long like maxDistance, searching for game objects with the right collision layer
        if (Physics.SphereCast(pivotPosition, cameraRadius, rayDirection, out RaycastHit hit, maxDistance, collisionLayers))
        {
            //Limit current distance 
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }

        //Calculate final position and rotation
        Vector3 desiredPosition = pivotPosition + (rayDirection * currentDistance);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(pivotPosition);


    }
}
