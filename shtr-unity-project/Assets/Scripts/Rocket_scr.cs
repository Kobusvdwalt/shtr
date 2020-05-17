using UnityEngine;
using System.Collections;

public class Rocket_scr : MonoBehaviour {

	public float speed;
	public int damage;
	Rigidbody rigid;
	public GameObject explosionPrefab;

	GameObject cam;
	void Start () {
		rigid = GetComponent<Rigidbody>();
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = transform.forward * speed;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Solid" || col.tag == "Enemy")
		{
			gameObject.AddComponent<TimedDestroy_scr>();
			GetComponent<TimedDestroy_scr>().time = 1;
			Destroy(this);
			Destroy(GetComponent<BoxCollider>());
			Destroy(GetComponent<Rigidbody>());
			Destroy(GetComponent<SpriteRenderer>());
			transform.Find("Particles").GetComponent<ParticleSystem>().Stop();

			GameObject a = Instantiate(explosionPrefab);
			a.transform.position = transform.position;

			cam.GetComponent<Camera_scr>().Shake(1);
		}
		if (col.tag == "Enemy")
		{
			col.GetComponent<Enemy_scr>().DoDamage(damage);
		}
	}
}
