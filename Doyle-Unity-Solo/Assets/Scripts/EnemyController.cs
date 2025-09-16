using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 Position;
    private Quaternion Rotation;
    public GameObject Ice;
    void Start()
    {
        
    }

    void Update()
    {
        transform.GetLocalPositionAndRotation(out Vector3 localPos, out Quaternion localRot);
        Position = localPos;
        Rotation = localRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            Instantiate(Ice, Position, Rotation);
            Destroy(collision.gameObject);
        }
    }
}
