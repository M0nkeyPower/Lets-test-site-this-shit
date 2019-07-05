using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public float health;



    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
		if (this.tag == "Enemy") 
		{
			GameObject.Find ("GameManager").GetComponent<WaveSpawner> ().removeDeadEnemy (this.gameObject);
		}
        Destroy(gameObject);
    }
}
