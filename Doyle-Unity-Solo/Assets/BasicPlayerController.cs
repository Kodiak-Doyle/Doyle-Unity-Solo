using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{

    public float mouseHorizontal = 2;
    public float mouseVertical = 2;

    public float Speed = 10;
    public Rigidbody Rb;

    private Vector3 Movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //xyz Movement
        float xAxis = Input.GetAxis("Horizontal") * Speed;
        float yAxis = Input.GetAxis("Vertical") * Speed;

        Movement = new Vector3(xAxis, 0, yAxis);
        Rb.linearVelocity = new Vector3(Movement.x, Movement.y, Movement.z);

        //Look at me go dad
        float lookH = mouseHorizontal * Input.GetAxis("Mouse X");
        float lookV = mouseVertical * Input.GetAxis("Mouse Y");

        Quaternion currentRotation = transform.rotation;


        transform.Rotate(-lookV, lookH, 0);
        //transform.rotation = Quaternion.Euler(lookV, lookH, 0);

    }
}
