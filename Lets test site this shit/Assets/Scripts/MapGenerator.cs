using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public GameObject cam;
	public int gridLenght;
	public int gridWidth;
	[Range(1,20)]
	public int borderOffset;
	[Range(1,100)]
	public float offsetX;
	[Range(1,100)]
	public float offsetY;
	public GameObject tilePrefab;
	public GameObject emptyTilePrefab;
	public GameObject startingUnitPrefab;
	GameObject tileClone;


	int[,] map;
	[Range(0,100)]
	public float percentage;
	public int cycles;
	List<GameObject> tiles;
	List<GameObject> spawnPoints;

	// Use this for initialization
	void Start () 
	{
		tiles = new List<GameObject> ();
		spawnPoints = new List<GameObject> ();
		map = new int[gridLenght + 1,gridWidth + 1];
		//generate basic grid
		int number;
		for(int i = 0; i <= gridLenght; i++)
		{
			for(int j = 0;j <= gridWidth; j++)
			{
				if (i <= borderOffset || i >= gridLenght - borderOffset || j <= borderOffset || j >= gridWidth - borderOffset) 
				{
					map [i, j] = 1;
				}
				else 
				{
					number = Random.Range (0, 100);
					if (number < percentage) {
						map [i, j] = 1;
					} else 
					{
						map [i, j] = 0;
					}
				}

			}
		}

		//grid cycle smooth
		for (int k = 0; k < cycles; k++) 
		{
			for(int i = 1; i < gridLenght; i++)
			{
				for(int j = 1; j < gridWidth; j++)
				{
					number = 0;
					//number += map [i - 1, j - 1] + map [i, j - 1] + map [i + 1, j - 1];
					//number += map [i - 1, j] + map [i + 1, j];
					//number += map [i,j + 1];
					number = getNeighbourTilesValue (i, j);
					if (number > 3)       
					{
						map [i, j] = 1;

					} 
					else if (number < 3) 
					{
						map [i, j] = 0;
					}


				}
			}
		}
			

		//grid instantiation
		Vector3 spawnPoint = new Vector3 ();
		spawnPoint.y = 0;

		for(int i = 0; i <= gridLenght; i++)
		{
			for(int j = 0;j <= gridWidth; j++)
			{
				
					if (j % 2 == 0) 
					{
						spawnPoint.x = i * offsetX + (offsetX/2); 
						spawnPoint.z = j * offsetY;
					} else 
					{
						spawnPoint.x = i * offsetX; 
						spawnPoint.z = j * offsetY;
					}

				if (map [i, j] > 0) 
				{
					tiles.Add(tileClone = Instantiate (tilePrefab, spawnPoint, Quaternion.identity) as GameObject);
				} else 
				{
					tiles.Add(tileClone = Instantiate (emptyTilePrefab, spawnPoint, Quaternion.identity) as GameObject);

				}
			}
		}
			

		spawnPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public int getNeighbourTilesValue(int x, int y)
	{
		int number = 0;
		number += (map[x - 1, y - 1] + map [x, y - 1] + map [x + 1, y - 1]) + (map [x - 1, y] + map [x + 1, y]) + (map [x,y + 1]);
		return number;
	}



	public void spawnPlayer()
	{
		foreach (var item in tiles) 
		{
			
			if (item.tag == "EmptyTile") 
			{
				int indexJ = Mathf.RoundToInt( item.transform.position.z / offsetY );
				int indexI;
				if (indexJ % 2 == 0) 
				{
					indexI =  Mathf.RoundToInt( (item.transform.position.x - (offsetX / 2)) / offsetX );
				} else
				{
					indexI =  Mathf.RoundToInt( item.transform.position.x / offsetX );
				}

				if (getNeighbourTilesValue (indexI, indexJ) == 0) 
				{
					spawnPoints.Add (item);
				}

			}
		}
		if (spawnPoints.Count > 0) 
		{
			int index = Random.Range (0, spawnPoints.Count - 1);
			Vector3 spawnPoint = new Vector3 ();
			spawnPoint.x = spawnPoints [index].transform.position.x;
			spawnPoint.y = spawnPoints [index].transform.position.y + 1;
			spawnPoint.z = spawnPoints [index].transform.position.z;

			tileClone = Instantiate (startingUnitPrefab, spawnPoint, Quaternion.identity) as GameObject;
			spawnPoint.z -= 20f;
			spawnPoint.y = 60f;
			//cam.transform.position = spawnPoint;
		}
			
	}



	public void tilesAdd(GameObject unit)
	{
		if (!tiles.Contains (unit)) 
		{
			tiles.Add (unit);
		}
	}

	public void tileRemove(GameObject unit)
	{
		tiles.Remove (unit);
	}

}
