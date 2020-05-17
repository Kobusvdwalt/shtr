using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGen_scr : MonoBehaviour {

	public int mapSize;
	public int barrelSpawnChance;
	public int walkerSpawnChance;
	public int eyeballSpawnChance;
	public int lazerbeamerSpawnChange;

	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public GameObject roofPrefab;
	public GameObject doorPrefab;
	public GameObject barrelPrefab;
	public GameObject machineGunPrefab;
	public GameObject pistolPrefab;
	public GameObject healthPrefab;
	public GameObject ammoPrefab;
	public GameObject eyeballPrefab;
	public GameObject walkerPrefab;
	public GameObject lazerbeamer;

	string[,] terrainMap = new string[64, 64];
	string[,] enemyMap = new string[64, 64];
	string[,] pickUpMap = new string[64, 64];

	void Start () {
		// Array prep
		for (int y=0; y < terrainMap.GetLength(1); y ++)
		{
			for (int x=0; x < terrainMap.GetLength(0); x ++)
			{
				terrainMap[x, y] = "0";
				enemyMap[x, y] = "0";
				pickUpMap[x, y] = "0";
			}
		}

		Generate(32, 32);
		Populate();

		// Object instanciation
		for (int y=1; y < terrainMap.GetLength(1)-1; y ++)
		{
			for (int x=1; x < terrainMap.GetLength(0)-1; x ++)
			{
				if (terrainMap[x,y] == "C" || terrainMap[x, y] == "D")
				{
					// Floor && roof
					GameObject a = Instantiate(floorPrefab);
					a.transform.position = new Vector3(x*6, 0, y*6);

					GameObject c = Instantiate(roofPrefab);
					c.transform.position = new Vector3(x*6, 6, y*6);


					// Wall
					if (terrainMap[x-1, y] == "0")
					{
						GameObject b = Instantiate(wallPrefab);
						b.transform.position = new Vector3(x*6 - 0.48f, 0.42f, y*6 + 6);
						b.transform.localEulerAngles = new Vector3(0, 90, 0);
					}
					if (terrainMap[x, y-1] == "0")
					{
						GameObject b = Instantiate(wallPrefab);
						b.transform.position = new Vector3(x*6, 0.42f, y*6 - 0.48f);
						b.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					if (terrainMap[x+1, y] == "0")
					{
						GameObject b = Instantiate(wallPrefab);
						b.transform.position = new Vector3(x*6 + 6, 0.42f, y*6 + 6);
						b.transform.localEulerAngles = new Vector3(0, 90, 0);
					}
					if (terrainMap[x, y+1] == "0")
					{
						GameObject b = Instantiate(wallPrefab);
						b.transform.position = new Vector3(x*6, 0.42f, y*6 + 6);
						b.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
				}

				// Door
				if (terrainMap[x,y] == "D")
				{
					if (terrainMap[x, y-1] == "0" && terrainMap[x, y+1] == "0")
					{
						GameObject a = Instantiate(doorPrefab);
						a.transform.position = new Vector3(x*6 + 2.5f, 0.42f, y*6 + 6);
						a.transform.localEulerAngles = new Vector3(0, 90, 0);
					}
					else
					if (terrainMap[x-1, y] == "0" && terrainMap[x+1, y] == "0")
					{
						GameObject a = Instantiate(doorPrefab);
						a.transform.position = new Vector3(x*6, 0.42f, y*6 + 2.5f);
						a.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
				}

				// Enemies
				if (enemyMap[x, y] == "Player")
				{
					GameObject.Find("Player").transform.position = new Vector3(x * 6 + 2.5f, 2, y * 6 + 2.5f);
				}
				if (enemyMap[x, y] == "Eyeball")
				{
					GameObject a = Instantiate(eyeballPrefab);
					a.transform.position = new Vector3(x*6 + 2.5f, 2, y*6 + 2.5f);
				}
				if (enemyMap[x, y] == "Walker")
				{
					GameObject a = Instantiate(walkerPrefab);
					a.transform.position = new Vector3(x*6 + 2.5f, 2, y*6 + 2.5f);
				}
				if (enemyMap[x, y] == "Lazerbeamer")
				{
					GameObject a = Instantiate(lazerbeamer);
					a.transform.position = new Vector3(x*6 + 2.5f, 2, y*6 + 2.5f);
				}

				// Pickups
				if (pickUpMap[x, y] == "Barrel")
				{
					GameObject a = Instantiate(barrelPrefab);
					a.transform.position = new Vector3(x*6 + Random.Range(0.5f, 5f), 0, y*6 + Random.Range(0.5f, 5f));
				}
			}
		}
	}
	int count = 0;
	void Generate (int startX, int startY)
	{
		int currX = startX;
		int currY = startY;
		int dirX = 0;
		int dirY = 1;
		int randomNum = 0;

		terrainMap[currX, currY] = "C";

		for (int i=0; i < mapSize; i ++)
		{
			// Pick direction;
			randomNum = Random.Range(0, 2);
			if (randomNum == 0)
			{
				int bucket = dirX;
				dirX = dirY;
				dirY = bucket;
			}
			else
			if (randomNum == 1)
			{
				int bucket = dirX;
				dirX = -dirY;
				dirY = -bucket;
			}

			for (int j=0; j < Random.Range(1, 5); j ++)
			{
				currX += dirX;
				currY += dirY;

				currX = Mathf.Clamp(currX, 1, 62);
				currY = Mathf.Clamp(currY, 1, 62);
				terrainMap[currX, currY] = "C";
			}
		}
		if (count < 3)
		{
			count ++;
			Generate(currX, currY);
		}
		else
		{
			for (int localY=-3; localY < 4; localY ++)
			{
				for (int localX=-3; localX < 4; localX ++)
				{
					int targetX = currX + localX;
					int targetY = currY + localY;

					targetX = Mathf.Clamp(targetX, 1, 62);
					targetY = Mathf.Clamp(targetY, 1, 62);

					enemyMap[targetX, targetY] = "EnemyDeadZone";
				}
			}

			for (int localY=-1; localY < 2; localY ++)
			{
				for (int localX=-1; localX < 2; localX ++)
				{
					int targetX = currX + localX;
					int targetY = currY + localY;

					targetX = Mathf.Clamp(targetX, 1, 62);
					targetY = Mathf.Clamp(targetY, 1, 62);

					pickUpMap[targetX, targetY] = "StartItemZone";
				}
			}
			enemyMap[currX, currY] = "Player";

			// Post process the map
			for (int i=0; i < 3; i ++)
			{
				for (int y=1; y < 62; y ++)
				{
					for (int x=1; x < 62; x ++)
					{   // Open space removal
						if (terrainMap[x, y] == "C" &&
							terrainMap[x+1, y] == "C" && terrainMap[x-1, y] == "C" &&
							terrainMap[x+1, y+1] == "C" && terrainMap[x-1, y-1] == "C" &&
							terrainMap[x, y+1] == "C" && terrainMap[x, y-1] == "C" &&
							terrainMap[x+1, y-1] == "C" && terrainMap[x-1, y+1] == "C")
						{
							terrainMap[x, y] = "0";
						}
						else // Door placement
						if (terrainMap[x, y] == "C" &&
							(terrainMap[x-1, y] == "0" && terrainMap[x+1, y] == "0" ||
							 terrainMap[x, y-1] == "0" && terrainMap[x, y+1] == "0"))
						{
							bool overlapsDoor = false;
							for (int localY=-2; localY < 3; localY ++)
							{
								for (int localX=-2; localX < 3; localX ++)
								{
									int targetX = x + localX;
									int targetY = y + localY;

									targetX = Mathf.Clamp(targetX, 1, 62);
									targetY = Mathf.Clamp(targetY, 1, 62);

									if (terrainMap[targetX, targetY] == "D")
									{
										overlapsDoor = true;
									}
								}
							}

							if (overlapsDoor == false && Random.Range(0, 100) < 80)
							{
								terrainMap[x, y] = "D";
							}
						}

					}
				}
			}
		}



	}

	bool playerBarrelPlaced = false;
	void Populate ()
	{
		for (int y=1; y < 62; y ++)
		{
			for (int x=1; x < 62; x ++)
			{
				if (terrainMap[x, y] == "C")
				{
					if (enemyMap[x, y] == "0")
					{						
						if (Random.Range(0, 100) < eyeballSpawnChance)
						{
							enemyMap[x, y] = "Eyeball";
						}
						if (Random.Range(0, 100) < walkerSpawnChance)
						{
							enemyMap[x, y] = "Walker";
						}
						if (Random.Range(0, 100) < lazerbeamerSpawnChange)
						{
							enemyMap[x, y] = "Lazerbeamer";
						}
					}

					if (Random.Range(0, 100) < barrelSpawnChance)
					{
						pickUpMap[x, y] = "Barrel";
					}
				}
				if (pickUpMap[x, y] == "StartItemZone" && (terrainMap[x, y] == "C"||terrainMap[x, y] == "D") && enemyMap[x, y] != "Player" && playerBarrelPlaced == false)
				{
					playerBarrelPlaced = true;
					pickUpMap[x, y] = "Barrel";
				}
			}
		}
	}

	void Update ()
	{
//		if (Input.GetKeyDown(KeyCode.R))
//		{
//			Application.LoadLevel(Application.loadedLevel);
//		}
	}
}
