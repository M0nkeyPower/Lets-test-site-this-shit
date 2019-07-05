using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour {


    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.gameObject.tag == "Enemy")
        {
            //Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(GetComponent<ProjectileControl>().damage);
            Destroy(gameObject,1);
        }
        else
        {
            GetComponent<TrailRenderer>().enabled = false;
            GetComponent<ProjectileControl>().enabled = false;
            rb.useGravity = true;
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject, 30);
        }
        
       
        
    }


}
