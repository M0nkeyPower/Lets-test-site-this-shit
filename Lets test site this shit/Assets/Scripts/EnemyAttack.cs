using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {


    public float damage;
    bool attack = false;
    GameObject player;
    public float timeBetweenAttacks;
    float lastAttack = 0f;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
		if (player == null)
		{
			attack = false;
		}
		Attack();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.GetComponent<HealthManager>().health > 0)
        {
            attack = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack = false;
        }
    }

    private void Attack()
    {
        if (attack)
        {
            float time = Time.fixedTime;
            if (lastAttack + timeBetweenAttacks < time)
            {
                player.GetComponent<PlayerGeneral>().TakeDamage(damage);
                lastAttack = time;
            }
        }
       
    }

}
