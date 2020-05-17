using UnityEngine;
using System.Collections;

public class Walker_scr : Enemy_scr {

	public float acceleration;
	public float maxSpeed;
	public float fireRate;
	public AudioClip deathClip;
	public GameObject bulletPrefab;

	Vector3 movementDirection;
	int fireRateCount = 0;

	GameObject sprite;
	void Start () {
		base.Start();
		sprite = transform.Find("Sprite").gameObject;
		acceleration *= Random.Range(0.8f, 1.3f);
		maxSpeed *= Random.Range(0.8f, 1.3f);
		InvokeRepeating("ChangeDirection", 2f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out hit))
		{}

		if (hit.collider.tag == "Player")
		{
			rigid.velocity = movementDirection * 9f;
			if (fireRateCount < 0)
			{
				fireRateCount = Mathf.RoundToInt(fireRate / Time.deltaTime);

				GameObject a = Instantiate(bulletPrefab);
				a.transform.position = transform.position;
				a.transform.LookAt(player.transform.position);
				a.GetComponent<Bullet_scr>().speed = 30;
				a.GetComponent<Bullet_scr>().damage = 10;

				GetComponent<AudioSource>().Play();
				sprite.transform.localScale += new Vector3(1f, -0.5f, 0);
			}

		}
		fireRateCount --;

		sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, Vector3.one, 0.3f);
	}

	public override void DoDamage (int damage)
	{
		base.DoDamage (damage);
		GameObject a = Instantiate(bloodSplatterPrefab);
		a.transform.position = transform.position;
		StartCoroutine(Flash(transform.Find("Sprite").gameObject));

		if (health < 0)
		{
			for (int i=0; i < 5; i ++)
			{
				GameObject b = Instantiate(gorePrefab);
				Vector3 dir =  new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				b.transform.position = transform.position + dir;
				b.GetComponent<Rigidbody>().AddForce(dir * 10, ForceMode.Impulse);

				GameObject c = Instantiate(audioGOPrefab);
				c.transform.position = transform.position;
				c.GetComponent<AudioSource>().clip = deathClip;
				c.GetComponent<AudioSource>().pitch = Random.Range(0.7f, 0.9f);
				c.GetComponent<AudioSource>().Play();
			}
		}
	}

	void ChangeDirection ()
	{
		movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
	}
}
