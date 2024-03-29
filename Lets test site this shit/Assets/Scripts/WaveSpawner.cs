﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {


   
    public int difficulty;
	public int spawnRate;
	public int spawnDelay;
	public int breakTime;

    public int numberOfWaves;
	private int currentWave;
	bool waveSpawned;


    public int baseNumberOfEnemies;
	private int amountOfUpcomingEnemies;
	//private int remainingEnemies;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
	private List<GameObject> enemies = new List<GameObject>();
	List<GameObject> spawnPoints;


	public Text currentWaveUI;
	public Text enemiesRemainingUI;
	public float distanceThreshold;


    // Use this for initialization
    void Start()
    {
		currentWave = 0;
		waveSpawned = true;
		//Assign spawnpoints here!!!
		//spawnPoints = FindObjectOfType<MapGenerator>().getSpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
		if (enemies.Count < 1 && currentWave < numberOfWaves && waveSpawned) 
		{
			
			currentWave++;
			waveSpawned = false;
			enemies.Clear ();
			amountOfUpcomingEnemies = baseNumberOfEnemies * currentWave;
			StartCoroutine(SpawnNextWave());
		
		}
		updateUI ();
    }

   

	IEnumerator SpawnNextWave()
	{	
		//wait before spawning next wave
		if (currentWave > 1)
		{
			yield return new WaitForSeconds(breakTime);
		}
	
		for (int y = 0; y < amountOfUpcomingEnemies; y++)
		{
			//spawn certain amount off enemies at once
			for (int j = 1; j <= spawnRate; j++) 
			{
				if (y < amountOfUpcomingEnemies) 
				{
					//Rework this section!!!
					GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0,2)], spawnPoints[Random.Range(0, spawnPoints.Count - 1)].gameObject.transform);
					enemies.Add (enemy);
					y += 1;
				}

			}
			y--;
			yield return new WaitForSeconds(spawnDelay);
		}
		//increase spawn rate for next wave
		spawnRate++;
		waveSpawned = true;
	}

	public void removeDeadEnemy(GameObject enemy)
	{
		enemies.Remove (enemy);
	}

	void updateUI()
	{
		currentWaveUI.text = "Wave: " + currentWave + "/" + numberOfWaves;


		enemiesRemainingUI.text ="Enemies: " + enemies.Count + "/" + amountOfUpcomingEnemies;
	}


	public void setSpawnPoints(List<GameObject> spawnPoints)
	{
		this.spawnPoints = spawnPoints;
	}

	public float getDistanceThreshold()
	{
		return distanceThreshold;
	}
   
}