using UnityEngine;
using System.Collections;

public class HealthPickup_scr : MonoBehaviour {

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
			GameObject audio = Instantiate(audioGOPrefab);
			audio.transform.position = transform.position;
			audio.GetComponent<AudioSource>().clip = sfx;
			audio.GetComponent<AudioSource>().Play();
			
			col.GetComponent<Player_scr>().health += 20;
			Destroy(gameObject);
		}
	}
}
