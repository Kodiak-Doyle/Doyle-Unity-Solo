using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Mouse Controls
    public float Xsensitivity = 2f;
    public float Ysensitivity = 2f;
    InputAction lookVector;
    Camera playerCamera;
    Vector2 cameraRotation;
    public float lookClamp = 90f;
    Transform camHolder;



    //Movement
    public float Speed = 10f;
    public Rigidbody Rb;
    float verticalMove;
    float horizontalMove;
    float yVelo;

    //Jumping
    public float jumpForce = 10f;
    private bool isGrounded;
    private int jumpsRem;
    public int baseJumps;

    //Wall Ride
    public bool onWall = false;
    public float wallDrag;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;


        //Camera
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        playerCamera = Camera.main;
        cameraRotation = Vector2.zero;
        camHolder = transform.GetChild(0);


        //Jumps
        jumpsRem = baseJumps;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;

       
    }

    void Update()
    {
        //lock cursor

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Go ahead and Jump
        if (Input.GetKeyDown("space"))
        {
            if (jumpsRem > 0)
            {
                if (isGrounded || onWall)
                {
                    Rb.AddForce(jumpForce * transform.up * 10);
                    jumpsRem--;
                }
            }
        }
       

        //Movement
        Vector3 temp = Rb.linearVelocity;

        temp.x = verticalMove * Speed;
        temp.z = horizontalMove * Speed;

        Rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);

        if (onWall == true)
        {
            Rb.AddForce(-wallDrag * transform.up * 5);
        }

        //Look at me go dad
        cameraRotation.y += lookVector.ReadValue<Vector2>().y * Ysensitivity;
        cameraRotation.x += lookVector.ReadValue<Vector2>().x * Xsensitivity;

        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -lookClamp, lookClamp);

        playerCamera.transform.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);
        transform.localRotation = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            jumpsRem = baseJumps;
        }
        if (collision.gameObject.tag == "Wall")
        {
            onWall = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            onWall = true;
            jumpsRem = baseJumps;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            onWall = false;
        }
    }
}
