using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Mouse Controls
    public float mouseSpeed = 2f;

    //Movement
    public float Speed = 10f;
    public Rigidbody Rb;
    float verticalMove;
    float horizontalMove;
    float yVelo;

    //Jumping
    public float jumpForce = 10f;
    private bool isGrounded;
    public int jumpsRem = 2;


    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;

       
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

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

        temp.x = verticalMove * Speed;
        temp.z = horizontalMove * Speed;

        Rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);

            //Look at me go dad
        float lookH = mouseSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, lookH, 0);


        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            jumpsRem = 2;
        }
        if (collision.gameObject.tag == "Wall")
        {
            Rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            jumpsRem = 2;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Rb.constraints ^= RigidbodyConstraints.FreezePositionY;
        }
        
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
