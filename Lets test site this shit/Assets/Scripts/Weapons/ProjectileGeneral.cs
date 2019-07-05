using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileGeneral : MonoBehaviour {




	private float damage;
	private float mass;
	private float speed;
	private float stumblePower;
	private float destroyDelayMultiplier;
	private Vector3 initialVelocity;
	private int speedTresholdDivider = 5;

	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = rb.velocity;
		rb.transform.LookAt (rb.transform.position + direction);
		if (rb.velocity.magnitude < speed / speedTresholdDivider)
		{
			Destroy (gameObject);
		}
	}


	public void setValues(float damage, float mass, float projectileSpeed, Vector3 initialVelocity, float stumblePower, float destroyDelayMultiplier)
	{
		this.damage += damage;
		this.mass = mass;
		this.speed = projectileSpeed;
		this.initialVelocity = initialVelocity;
		this.stumblePower = stumblePower;
		this.destroyDelayMultiplier = destroyDelayMultiplier;
	}


	public void fire()
	{
		rb = GetComponent<Rigidbody> ();
		rb.velocity = initialVelocity + transform.forward * speed;
		Destroy (this.gameObject, destroyDelayMultiplier);
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
