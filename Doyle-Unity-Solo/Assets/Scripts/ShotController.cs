using UnityEngine;
using System.Collections;


public class ShotController : MonoBehaviour
{
    public GameObject Projectile;
    private Vector3 Pos;
    private Quaternion Rot;
    public float shotDelay;
    private bool canShoot = true;

    void Start()
    {
        
    }

    void Update()
    {

        Pos = transform.position;
        Rot = transform.rotation;


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot == true)
            {
                Instantiate(Projectile, Pos, Rot);
                canShoot = false;
                StartCoroutine(Delay());
            }
            
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }
}
