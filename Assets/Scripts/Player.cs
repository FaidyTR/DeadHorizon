using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody playerRigidbody;

    [SerializeField] private GameObject camera;
    [SerializeField] private float mouseSensitivity = .1f;
    private float cameraYaw;
    private float cameraPitch;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerInputManager.OnJump += HandleJump;
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void HandleJump(object sender, System.EventArgs e)
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Player Jumped!");
    }
    private void Move()
    {
        Vector2 inputVector = playerInputManager.GetInputVector2();

        Vector3 cameraForword = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;

        cameraForword.y = 0;
        cameraRight.y = 0;

        cameraForword.Normalize();
        cameraRight.Normalize();



        Vector3 moveDirection = cameraForword * inputVector.y + cameraRight * inputVector.x ;
        gameObject.transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;
    }
    private void Look()
    {
        Vector2 lookInputVector = playerInputManager.GetInputLookDirection();
        
        cameraYaw += lookInputVector.x * mouseSensitivity;
        cameraPitch -= lookInputVector.y * mouseSensitivity;

        if (cameraPitch > 90) cameraPitch = 90;
        if (cameraPitch < -90) cameraPitch = -90;

        camera.transform.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0 );
        gameObject.transform.rotation = Quaternion.Euler(0, cameraYaw, 0);
        

    }



}
