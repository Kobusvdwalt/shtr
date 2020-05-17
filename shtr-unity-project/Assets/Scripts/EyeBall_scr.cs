using UnityEngine;
using System.Collections;

public class EyeBall_scr : Enemy_scr {
	
	public float acceleration;
	public float maxSpeed;
	public AudioClip deathClip;

	new void Start () {
		base.Start();
		acceleration *= Random.Range(0.8f, 1.3f);
		maxSpeed *= Random.Range(0.8f, 1.3f);
	}

	new void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out hit))
		{}

		if (hit.collider.tag == "Player")
		{
			if (rigid.velocity.magnitude < maxSpeed)
			{
				rigid.AddForce(-(transform.position - player.transform.position).normalized * acceleration);
			}
			else
			{
				rigid.AddForce(-rigid.velocity * acceleration);
			}
		}
		else
		{
			rigid.AddForce(-rigid.velocity * acceleration);
		}	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player")
		{
			col.GetComponent<Player_scr>().DoDamage(15);
			rigid.AddForce((transform.position - player.transform.position).normalized * 50, ForceMode.Impulse);
		}
	}
	public override void DoDamage (int damage)
	{
		base.DoDamage (damage);
		GameObject a = Instantiate(bloodSplatterPrefab);
		a.transform.position = transform.position;
		StartCoroutine(Flash(gameObject));

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


}
