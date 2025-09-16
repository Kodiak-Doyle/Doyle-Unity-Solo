using UnityEngine;
using System.Collections;


public class Projectile : MonoBehaviour
{
    Rigidbody Rb;
    public float speed;
    public float lifetime;
    bool HasShot;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        StartCoroutine(Death());
    }

    void Update()
    {

        if(HasShot == false)
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
        Destroy(gameObject);
    }

}
