using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Must be present on a character in order to use weapons
/// Represents weapon set of a character
/// Gets a weapon (type of scriptaple object) and sets values acordingly
/// handles firing and weapon switching
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class WeaponGeneral : MonoBehaviour {


	//values that weapon gets from weapon scriptable object
	private string weaponName;
	private float fireRate;
	private int magazineCapacity;
	private int projectilesPerShot;

	private float accuracy;
	private int divisionFactor;
	private const float ACCURACY_TRESHOLD = 100;

	//values that projectile inherits
	private float damage;
	private float projectileSpeed;
	private float stumblePower;
	private float destroyDelay;
	private int fireType;
	private bool alternateFire;
	private int alternatefireType;
	private Mesh mesh;


	private Vector3 initialVelocity;

	//fire variables
	private float time;
	private float lastFired = 0f;

	//scribtable objects to inherit values from
	public List<Weapon> weapons;
	private int currentWeapon;
	private int currentProjectile;

	//place where projectile are intantiated
	//private Transform firePoint;

	//projectile prefab that is created later via script
	private GameObject projectile;
	//players rigidbody
	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		currentWeapon = 0;
		currentProjectile = 0;

		rb = this.GetComponent<Rigidbody> ();
		accuracy = ACCURACY_TRESHOLD - weapons[currentWeapon].accuracy;
	}
	
	// Update is called once per frame
	void Update () 
	{




		#region weapon switch
		//weapon switch
		if (Input.GetAxis ("WeaponSwith") != 0)
		{
			if (Input.GetAxis ("WeaponSwith") > 0)
			{
				currentWeapon--;	
			} else
			{
				currentWeapon++;
			}

			if (currentWeapon < 0)
			{
				currentWeapon = weapons.Count - 1;
			} else if (currentWeapon >= weapons.Count)
				{
					currentWeapon = 0;				
				}
			//when weapon is swithed --> set to default projectile
			currentProjectile = 0;
			//load weapon atributes, probably dont need to do that
			accuracy = ACCURACY_TRESHOLD - weapons[currentWeapon].accuracy;
			/*
			weaponName = weapons[currentWeapon].weaponName;
			fireRate = weapons[currentWeapon].fireRate;
			magazineCapacity = weapons[currentWeapon].magazineCapacity;
			projectilesPerShot = weapons[currentWeapon].projectilesPerShot;
			divisionFactor = weapons[currentWeapon].divisionFactor;
			damage = weapons[currentWeapon].damage;
			projectileSpeed = weapons[currentWeapon].projectileSpeed;
			stumblePower = weapons[currentWeapon].stumblePower;
			destroyDelay = weapons[currentWeapon].destroyDelay;
			fireType = weapons[currentWeapon].fireType;;
			alternateFire = weapons[currentWeapon].alternateFire;;
			alternatefireType = weapons[currentWeapon].alternatefireType;;
			*/

		}
		#endregion


		#region projectile switch
		if(Input.GetKeyDown(KeyCode.Q))
		{
			currentProjectile++;	
		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			currentProjectile--;	
		}

		if (currentProjectile < 0)
		{
			currentProjectile = weapons[currentWeapon].projectiles.Count - 1;
		} else if (currentProjectile >= weapons[currentWeapon].projectiles.Count)
		{
			currentProjectile = 0;				
		}
		#endregion



		#region Firing
		if(Input.GetAxis("Fire1") > 0)
		{
			fire(fireType);
		}
		else if(Input.GetAxis("Fire2") > 0)
		{
			if(alternateFire)
			{
				fire(alternatefireType);
			}

		}
		#endregion

	}


	private void createProjectile()
	{
		//creating a new projectile
		projectile = new GameObject ();
		projectile.transform.position = transform.position + (transform.forward * 2);
		projectile.transform.rotation = transform.rotation;
		//set name of the gameobject
		projectile.name = weapons[currentWeapon].projectiles[currentProjectile].name;
		//add rigidbody to the projectile
		projectile.AddComponent<Rigidbody>();
		projectile.GetComponent<Rigidbody> ().useGravity = false;
		projectile.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;


		//set mesh of the projectile
		projectile.AddComponent<MeshFilter> ();
		projectile.GetComponent<MeshFilter>	().mesh = weapons [currentWeapon].projectiles [currentProjectile].mesh;
		//add mesh renderer
		projectile.AddComponent<MeshRenderer>();
		projectile.GetComponent<MeshRenderer> ().material = weapons [currentWeapon].projectiles [currentProjectile].material;
		//set scaling
		Vector3 scale = new Vector3(weapons [currentWeapon].projectiles [currentProjectile].scaleX,weapons [currentWeapon].projectiles [currentProjectile].scaleY,weapons [currentWeapon].projectiles [currentProjectile].scaleZ);
		projectile.transform.localScale = scale;
		//create collider of the projectile
		projectile.AddComponent<SphereCollider>();
		//set layer of the projectile
		projectile.layer = LayerMask.NameToLayer("Projectile");


		//add projectileGeneral script for behaviour
		projectile.AddComponent<ProjectileGeneral>();
	
		float damage = weapons [currentWeapon].damage + weapons [currentWeapon].projectiles [currentProjectile].damage;
		float mass =  weapons[currentWeapon].projectiles[currentProjectile].mass;
		float projectileSpeed = weapons[currentWeapon].projectileSpeed * weapons[currentWeapon].projectiles[currentProjectile].speedMultiplier;
		Vector3 initialVelocity = rb.velocity;
		float stumblePower = weapons[currentWeapon].stumblePower + weapons[currentWeapon].projectiles[currentProjectile].stumblePower;
		float destroyDelay = weapons[currentWeapon].destroyDelay + weapons[currentWeapon].projectiles[currentProjectile].destroyDelayMultiplier;

		projectile.GetComponent<ProjectileGeneral> ().setValues (damage, mass, projectileSpeed, initialVelocity, stumblePower, destroyDelay);
		projectile.transform.Rotate(Vector3.up,Random.Range (accuracy/weapons[currentWeapon].divisionFactor, -accuracy/weapons[currentWeapon].divisionFactor));
		projectile.GetComponent<ProjectileGeneral> ().fire ();
	

	}

	public void fire(int typeOfFire)
	{
		initialVelocity = rb.velocity;
	
		switch (typeOfFire)
		{
			case 0:

				Firetype0 ();
				break;

			case 1:
				break;

			case 2:
				break;

			default:
				break;
		}

	}

	private void Firetype0 ()
	{
		time = Time.fixedTime;
		if (lastFired + weapons[currentWeapon].fireRate < time)
		{
			for (int i = 0; i < weapons[currentWeapon].projectilesPerShot; i++)
			{
				createProjectile ();
				//GameObject bullet = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
				//bullet.GetComponent<ProjectileGeneral> ().setValues (weapons[currentWeapon].damage + weapons[currentWeapon].projectiles[currentProjectile].damage, weapons[currentWeapon].projectiles[currentProjectile].mass, weapons[currentWeapon].projectileSpeed + weapons[currentWeapon].projectiles[currentProjectile].speed, initialVelocity, weapons[currentWeapon].stumblePower + weapons[currentWeapon].projectiles[currentProjectile].stumblePower, weapons[currentWeapon].destroyDelay + weapons[currentWeapon].projectiles[currentProjectile].destroyDelay);
				//bullet.transform.Rotate(Vector3.up,Random.Range (accuracy/weapons[currentWeapon].divisionFactor, -accuracy/weapons[currentWeapon].divisionFactor));
			}
			lastFired = time;
		}
	}



}
