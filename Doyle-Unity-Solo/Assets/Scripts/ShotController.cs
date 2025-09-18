using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    public GameObject Projectile;
    private Vector3 Pos;
    private Quaternion Rot;
    public float shotDelay;
    private bool canShoot = true;

    public Slider indicator;
    public float FillSpeed;

    public bool hardMode;

    void Start()
    {
        
    }

    void Update()
    {

        if(canShoot == false)
        {
            indicator.gameObject.SetActive(true);
            indicator.value += FillSpeed * Time.deltaTime;
        }
        else
        {
            indicator.value = 0;
            indicator.gameObject.SetActive(false);
        }

        Pos = transform.position;
        Rot = transform.rotation;


        if (Input.GetKeyDown(KeyCode.Mouse0) && hardMode != true)
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
