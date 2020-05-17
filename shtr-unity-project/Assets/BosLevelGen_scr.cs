using UnityEngine;
using System.Collections;

public class BosLevelGen_scr : MonoBehaviour {

	public int sizeX;
	public int sizeY;
	public GameObject bossPrefab;
	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public GameObject roofPrefab;
	public GameObject doorPrefab;
	public GameObject barrelPrefab;

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

		// Array prep
		for (int y=1; y < sizeY; y ++)
		{
			for (int x=1; x < sizeX; x ++)
			{
				terrainMap[x, y] = "C";

				if (y == 1 || y == sizeY-1 || x == 1 || x == sizeX-1)
				{
					pickUpMap[x, y] = "Barrel";
				}
			}
		}
		
		// Object instanciation
		GameObject.Find("Player").transform.position = new Vector3(1 * 6 + 2.5f, 2, 1 * 6 + 2.5f);

		GameObject boss = Instantiate (bossPrefab);
		boss.transform.position = new Vector3(sizeX/2 * 6, 2, sizeY/2 * 6);

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

				// Pickups
				if (pickUpMap[x, y] == "Barrel")
				{
					GameObject a = Instantiate(barrelPrefab);
					a.transform.position = new Vector3(x*6 + Random.Range(0.5f, 5f), 0, y*6 + Random.Range(0.5f, 5f));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
