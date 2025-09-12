using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Mouse Controls
    public float Xsensitivity = 2f;
    public float Ysensitivity = 2f;
    InputAction lookVector;
    Transform playerCamera;
    Vector2 cameraRotation;
    public float lookClamp = 90f;
    Transform camHolder;

    //Respawn
    public Vector3 respawnPoint;


    //Movement
    public float currentSpeed = 7.5f;
    private float baseSpeed;
    public Rigidbody Rb;
    float verticalMove;
    float horizontalMove;
    float yVelo;

    //Jumping
    public float jumpForce = 10f;
    private bool isGrounded;
    public int jumpsRem;
    public int baseJumps;

    //Wall Ride
    public bool onWall = false;

    void Start()
    {


        Rb = GetComponent<Rigidbody>();

        //Variable set
        jumpsRem = baseJumps;
        baseSpeed = currentSpeed;
        respawnPoint = new Vector3(0, 1, 0);
        //cursor lock
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;




        //Camera
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        playerCamera = transform.GetChild(0);
        cameraRotation = Vector2.zero;
        camHolder = transform.GetChild(0);



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

        

        //Go ahead and Jump
        if (Input.GetKeyDown("space"))
        {
            if (jumpsRem > 0)
            {

               
                    Rb.AddForce(jumpForce * transform.up * 10);
                    jumpsRem--;
        
            }
        }


                //Movement
                Vector3 temp = Rb.linearVelocity;

        temp.x = verticalMove * currentSpeed;
        temp.z = horizontalMove * currentSpeed;

        Rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);

      

        //Look at me go dad
        cameraRotation.y += lookVector.ReadValue<Vector2>().y * Ysensitivity;
        cameraRotation.x += lookVector.ReadValue<Vector2>().x * Xsensitivity;

        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -lookClamp, lookClamp);

       playerCamera.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);
       transform.localRotation = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);

        //Death
        if(transform.position.y <= -25)
        {
            Death();
        }

    }

    private void Death()
    {
        transform.position = respawnPoint;

        //Scene Reload Option
            //Remove TP system
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            jumpsRem = baseJumps;

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
