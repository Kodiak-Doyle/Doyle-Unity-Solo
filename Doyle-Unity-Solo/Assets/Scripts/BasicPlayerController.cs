using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{

    public float mouseSpeed = 2;
    //public float mouseVertical = 2;

    public float Speed = 10;
    public Rigidbody Rb;

    public float Gravity;
    public float jumpForce;


    //private Vector3 zMAxis;
    private Vector3 Movement;


    void Start()
    {
        Rigidbody Rb = GetComponent<Rigidbody>();
    }



    void Update()
    {
        //Go ahead and Jump
        if (Input.GetKeyDown("space"))
        {
        Rb.AddForce(transform.up * jumpForce);
        }
        //Rb.AddForce(transform.up * -Gravity);

            //xyz Movement
        float xAxis = Input.GetAxis("Horizontal") * Speed;
        float yAxis = Input.GetAxis("Vertical") * Speed;

        Movement = new Vector3(xAxis, 0, yAxis);
        Rb.linearVelocity = new Vector3(Movement.x, 0, Movement.z);

            //Look at me go dad
        float lookH = mouseSpeed * Input.GetAxis("Mouse X");
        //float lookV = mouseVertical * Input.GetAxis("Mouse Y");

        //Quaternion currentRotation = transform.rotation;
        //zMAxis.z = 0;

        transform.Rotate(0, lookH, 0);
        //transform.rotation = Quaternion.Euler(zMAxis);

    }
}
