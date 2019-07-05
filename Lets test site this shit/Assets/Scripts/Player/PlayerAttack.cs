using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public float damage;
    public GameObject firePosLeft;
    public GameObject firePosRight;
    public GameObject projectile;
    public Light flashL;
    public Light flashR;
    public float fireRate;
    float lastFiredLeft = 0f;
    float lastFiredRight = 0f;
    float time;


    private void Start()
    {
        projectile.GetComponent<ProjectileControl>().damage = damage;
    }

    private void Update()
    {
        Fire();
    }





    public void Fire()
    {
        time = Time.fixedTime;
        if (lastFiredLeft + 0.1f < time)
        {
            flashL.gameObject.SetActive(false);
        }
        if (lastFiredRight + 0.1f < time)
        {
            flashR.gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0))
        {
            if (lastFiredLeft + fireRate < time)
            {
                flashL.gameObject.SetActive(true);
                Instantiate(projectile, firePosLeft.transform.position, firePosLeft.transform.rotation);
                lastFiredLeft = time;

            }
        }
        if (Input.GetMouseButton(1))
        {
            if (lastFiredRight + fireRate < time)
            {
                flashR.gameObject.SetActive(true);
                Instantiate(projectile, firePosRight.transform.position, firePosRight.transform.rotation);
                lastFiredRight = time;
            }
        }
    }

}
