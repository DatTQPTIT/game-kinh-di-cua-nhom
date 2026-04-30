using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float mouseSensitivity = 200.0f;
    [SerializeField] private float jumpForce = 1.0f;
    private bool isGrounded;

    [Header("References")]
    [SerializeField] private Transform playerCamera;

    private Rigidbody rb;
    private float horizontalInput;
    private float forwardInput;
    private float mouseXInput;
    private float mouseYInput;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        rb.freezeRotation = true;

        if(playerCamera == null ) playerCamera = GetComponentInChildren<Camera>().transform;
    }
    void Update()
    {
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //xoay camera
        xRotation -= mouseYInput * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Kiem tra cham dat
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        //Xoay trai phai
        transform.Rotate(Vector3.up * mouseXInput * mouseSensitivity * Time.deltaTime);

        //xu li nhay
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //tac dong luc huong len tren = nhay
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void FixedUpdate()
    {
        //Tinh toan huong di chuyen
        Vector3 moveDirection = transform.forward * forwardInput + transform.right * horizontalInput;

        //movedirX*speed, movedirZ*speed -> huong di theo truc x va z ; rb.linearVelocity.y -> theo trong luc va nhay
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }
}
