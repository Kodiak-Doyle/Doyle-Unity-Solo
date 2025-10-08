using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Vector3 Position;
    private Quaternion Rotation;
    private Vector3 Scale;
    public GameObject Ice;
    void Start()
    {

    }

    void Update()
    {
        transform.GetLocalPositionAndRotation(out Vector3 localPos, out Quaternion localRot);
        Position = localPos;
        Rotation = localRot;

        Scale = transform.lossyScale;
        Scale.x *= 1;
        Scale.y *= 12;
        Scale.z *= 1;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {

            GameObject newObject = Instantiate(Ice, Position, Rotation) as GameObject;
            newObject.transform.localScale = Scale;
            Destroy(gameObject);
            Destroy(collision.gameObject);

        }
    }
}
