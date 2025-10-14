using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    public GameObject Projectile;
    private Vector3 Pos;
    private Quaternion Rot;
    public float shotDelay;
    public bool canShoot = true;

    public bool isReloading = false;

    public Slider indicator;
    public float FillSpeed;

    public bool hardMode;

    public GameObject ShotPos;

    private Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();

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

        Pos = ShotPos.transform.position;
        Rot = transform.rotation;


        if (Input.GetKeyDown(KeyCode.Mouse0) && hardMode != true)
        {
            if (canShoot == true)
            {
                Instantiate(Projectile, Pos, Rot);
                canShoot = false;
                isReloading = true;
                StartCoroutine(Delay());
                myAnim.SetBool("isAttacking", true);
            }
            
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(shotDelay);
        isReloading = false;
        canShoot = true;
        myAnim.SetBool("isAttacking", false);
    }
}
