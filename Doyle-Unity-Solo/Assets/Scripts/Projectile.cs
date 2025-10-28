using UnityEngine;
using System.Collections;


public class Projectile : MonoBehaviour
{
    Rigidbody Rb;
    public float speed;
    public float lifetime;
    bool HasShot;
    public GameObject particles;
   // private Vector3 Position;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        StartCoroutine(Death());
    }

    void Update()
    {
        //Position = transform.position;
        if (HasShot == false)
        {
            Rb.AddForce(speed * transform.up * 10);
            HasShot = true;
        }

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
