using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSpeed = 2;
    //public Rigidbody rb;



    void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
    }



    void Update()
    {

        float lookV = mouseSpeed * Input.GetAxis("Mouse Y");


        transform.Rotate(-lookV, 0, 0);

    }

}


