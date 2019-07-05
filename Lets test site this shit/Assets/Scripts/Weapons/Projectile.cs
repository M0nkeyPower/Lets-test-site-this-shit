using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Projectile",  menuName = "Projectile")]
public class Projectile : ScriptableObject {


	public string projectileName;
	public float damage;
	public float mass;
	public float speedMultiplier;
	public float stumblePower;
	public float destroyDelay;
	public Mesh mesh;
	public Material material;
	public float scaleX;
	public float scaleY;
	public float scaleZ;

}
