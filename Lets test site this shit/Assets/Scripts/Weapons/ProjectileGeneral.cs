using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileGeneral : MonoBehaviour {




	private float damage;
	private float mass;
	private float speed;
	private float stumblePower;
	private float destroyDelay;
	private Vector3 initialVelocity;

	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		

	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}


	public void setValues(float damage, float mass, float projectileSpeed, Vector3 initialVelocity, float stumblePower, float destroyDelay)
	{
		this.damage += damage;
		this.mass = mass;
		this.speed = projectileSpeed;
		this.initialVelocity = initialVelocity;
		this.stumblePower = stumblePower;
		this.destroyDelay = destroyDelay;
	}


	public void fire()
	{
		rb = GetComponent<Rigidbody> ();
		rb.velocity = initialVelocity + transform.forward * speed;
		Destroy (this.gameObject, destroyDelay);
	}

	private void OnCollisionEnter(Collision collision)
	{

		if (collision.collider.gameObject.tag == "Enemy")
		{
			//Debug.Log(collision.gameObject.name);
			collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
			Destroy(gameObject);
		}
		else if(collision.collider.gameObject.tag != this.tag)
		{
			Destroy(gameObject);
		}
	}




}
