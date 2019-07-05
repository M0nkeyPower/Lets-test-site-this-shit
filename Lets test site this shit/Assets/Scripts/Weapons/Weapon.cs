using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon",  menuName = "Weapon")]
public class Weapon : ScriptableObject {
	

	/*
	 * Holds informations about weapon atributes and callable methods
	 * 
	*/
	public string weaponName;
	public float fireRate;
	public int magazineCapacity;
	public int projectilesPerShot;

	[Range(0,100)]
	public float accuracy;
	[Range(1,10)]
	public int divisionFactor;


	//values that projectile inherits
	public float damage;
	public float projectileSpeed;
	public float stumblePower;
	public float destroyDelay;

	public Mesh mesh;

	[Range(0,1)]
	public int fireType;
	public bool alternateFire;
	[Range(0,1)]
	public int alternatefireType;

	public float scaleX;
	public float scaleY;
	public float scaleZ;



	public List<Projectile> projectiles;

}
