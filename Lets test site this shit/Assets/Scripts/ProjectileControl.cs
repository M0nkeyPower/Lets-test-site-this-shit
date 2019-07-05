using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {


    public float speed;
    Rigidbody rb;
    public float damage;

	public float destroyDelay;
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

	void Update()
	{
		Destroy (this.gameObject, destroyDelay);
	}
	

}
