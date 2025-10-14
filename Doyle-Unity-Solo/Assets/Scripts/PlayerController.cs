using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //Mouse Controls
    Camera playerCamera;

    //Respawn
    public Vector3 respawnPoint;

    private bool checkpointSet = false;


    //Movement
    public float Speed = 10f;
    public Rigidbody Rb;
    float verticalMove;
    float horizontalMove;
    float yVelo;

    //Jumping
    public float jumpForce = 10f;
    public int jumpsRem;
    private int baseJumps = 2;

    //Wall Ride
    public bool onWall = false;
    public float wallDrag;

    public GameManager GM;


    void Start()
    {


        //variable set
        Rb = GetComponent<Rigidbody>();
        //respawnPoint = new Vector3(0, 1, 0);
        jumpsRem = baseJumps;
        Ray rayCast = new Ray(transform.position, transform.forward);


        //cursor lock
        Cursor.lockState = CursorLockMode.Locked;


        //Camera
        playerCamera = Camera.main;


        //Jumps
        jumpsRem = baseJumps;

        //Checkpoint
        transform.position = SceneMaster.active.currentCheckpoint.transform.position;

    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;

       
    }

    void Update()
    {
        


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

        if (onWall == true)
        {
            Rb.AddForce(-wallDrag * transform.up * 5);
        }

        //Look at me go dad

        Quaternion playerRotation = playerCamera.transform.rotation;
        playerRotation.x = 0;
        playerRotation.z = 0;
        transform.localRotation = playerRotation;

        //Death
        if (transform.position.y <= -25)
        {
            GM.Lose();
        }

        //Raycasting
    }

    public void Death()
    {
        if (checkpointSet == true)
        {
            transform.position = respawnPoint;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpsRem = baseJumps;
        }
        if (collision.gameObject.tag == "Wall")
        {
            onWall = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GM.Lose();
        }
        if (collision.gameObject.tag == "WinPlatform")
        {
            GM.LoadLevel(GM.currentLevel + 1);
        }
        if (collision.gameObject.tag == "FinalWin")
        {
            GM.Win();
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
