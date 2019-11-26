using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {


	Transform player = null;
	float distanceThreshold;
    NavMeshAgent nav;
	NavMeshPath path;

	// Use this for initialization
	void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
		path = new NavMeshPath ();
		if (GameObject.FindGameObjectWithTag ("Player") != null) 
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
		distanceThreshold = FindObjectOfType<WaveSpawner> ().getDistanceThreshold ();
      

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player != null) 
		{
			if ( (Vector3.Distance (nav.destination, player.transform.position) > distanceThreshold) && (Vector3.Distance(this.transform.position,player.position) > distanceThreshold) )
			{
				nav.CalculatePath (player.position, path);
				nav.SetPath(path);							
			}

		}
      
	}
}
