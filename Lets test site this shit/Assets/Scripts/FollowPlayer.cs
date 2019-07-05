using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {


	Transform player = null;
    public Transform hrac;
    NavMeshAgent nav;

	// Use this for initialization
	void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
		if (GameObject.FindGameObjectWithTag ("Player") != null) 
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
      

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player != null) 
		{
			nav.SetDestination(player.position);
		}
      
	}
}
