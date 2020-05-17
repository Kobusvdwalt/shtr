using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrel_scr : MonoBehaviour {

	public GameObject healthPrefab;
	public GameObject ammoPrefab;
	public GameObject[] gunPrefabs;
	public GameObject[] pickupPrefabs;
	public GameObject audioGOPrefab;
	public AudioClip sfx;
	int health = 10;

	GameObject player;
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DoDamage (int damage)
	{
		health -= damage;
		if (health < 0)
		{
			GameObject audio = Instantiate(audioGOPrefab);
			audio.transform.position = transform.position;
			audio.GetComponent<AudioSource>().clip = sfx;
			audio.GetComponent<AudioSource>().Play();

			if (player.GetComponent<Player_scr>().gun == null)
			{
				GameObject a = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Length)]);
				a.transform.position = transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 1, Random.Range(-0.2f, 0.2f));
			}
			else
			{
				if (Random.Range(0, 100) < 30)
				{
					GameObject a = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Length)]);
					a.transform.position = transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 1, Random.Range(-0.2f, 0.2f));
				}
			}

			if (player.GetComponent<Player_scr>().health < player.GetComponent<Player_scr>().maxHealth/4)
			{
				for (int i=0; i < Random.Range(2, 3); i ++)
				{
					GameObject a = Instantiate(healthPrefab);
					a.transform.position = transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0.5f, Random.Range(-0.2f, 0.2f));
				}
			}
			else
			{
				if (Random.Range(0, 100) < 30)
				{
					GameObject a = Instantiate(healthPrefab);
					a.transform.position = transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0.5f, Random.Range(-0.2f, 0.2f));
				}
			}

			for (int i=0; i < Random.Range(0, 3); i ++)
			{
				GameObject a = Instantiate(ammoPrefab);
				a.transform.position = transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0.5f, Random.Range(-0.2f, 0.2f));
			}

			Destroy(gameObject);
		}
	}
}
