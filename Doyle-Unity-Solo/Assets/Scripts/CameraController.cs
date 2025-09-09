using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MouseSpeed = 2;
    //public Rigidbody rb;



    void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
    }



    void Update()
    {

        float lookV = MouseSpeed * Input.GetAxis("Mouse Y");


        transform.Rotate(-lookV, 0, 0);

    }

}


