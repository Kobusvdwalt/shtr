using UnityEngine;
using System.Collections;

public class AmmoPickup_scr : MonoBehaviour {

	public GameObject audioGOPrefab;
	public AudioClip sfx;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Find("Sprite").localEulerAngles += new Vector3(0, 10, 0);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player")
		{
			if (col.GetComponent<Player_scr>().gun != null)
			{
				GameObject audio = Instantiate(audioGOPrefab);
				audio.transform.position = transform.position;
				audio.GetComponent<AudioSource>().clip = sfx;
				audio.GetComponent<AudioSource>().Play();

				col.GetComponent<Player_scr>().gun.GetComponent<Gun_scr>().ammo += 20;
				Destroy(gameObject);
			}
		}
	}
}
