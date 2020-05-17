using UnityEngine;
using System.Collections;

public class Lazerbeamer_scr : Enemy_scr {

	public AudioClip deathClip;
	string state = "Invis"; // Invis, Visible, Firing
	GameObject sprite;
	Vector3 movementDirection;
	LineRenderer line;
	Vector3 targetPos;
	Vector3 targetSize;
	int damageCount = 0;
	int repositionCount = 0;
	void Start () {
		base.Start();
		line = GetComponent<LineRenderer>();
		InvokeRepeating("ChangeDirection", 2f, 1f);
		sprite = transform.Find("Sprite").gameObject;
		StartCoroutine(Invis());
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "Invis")
		{
			rigid.velocity = movementDirection * 12f;
		}
		if (state == "Firing")
		{
			sprite.transform.localScale += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 0.5f;
			rigid.velocity = Vector3.zero;
			line.SetWidth(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));

			RaycastHit[] hit;

			hit = Physics.RaycastAll(transform.position, -(transform.position - targetPos).normalized, Vector3.Distance(transform.position, targetPos));
			for (int i=0; i < hit.Length; i ++)
			{
				if (hit[i].collider.tag == "Player")
				{
					if (damageCount < 0)
					{
						damageCount = 5;
						player.GetComponent<Player_scr>().DoDamage(3);
					}
					damageCount --;

					i = hit.Length;
				}
			}

		}
		if (state == "Visible")
		{
			rigid.velocity = Vector3.zero;
		}
		sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, targetSize, 0.3f);
	}
	IEnumerator Invis ()
	{
		state = "Invis";
		targetSize = Vector3.zero;
		line.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("Inactive");
		yield return new WaitForSeconds(Random.Range(1f, 2f));
		StartCoroutine(Visible());
		gameObject.layer = LayerMask.NameToLayer("Default");

	}
	IEnumerator Firing ()
	{
		
		state = "Firing";
		sprite.SetActive(true);
		line.enabled = true;

		RaycastHit[] hit;
		hit = Physics.RaycastAll(transform.position, -(transform.position - player.transform.position).normalized, 50f);
		for (int i=0; i < hit.Length; i ++)
		{
			if (hit[i].collider.tag == "Solid")
			{
				line.SetPosition(0, transform.position);
				line.SetPosition(1, hit[i].point);
				targetPos = hit[i].point;
				i = hit.Length;
			}
		}

		yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));
		StartCoroutine(Visible());
	}
	IEnumerator Visible ()
	{
		targetSize = Vector3.one;
		if (state == "Invis")
		{
			state = "Visible";
		//	sprite.SetActive(true);
			line.enabled = false;

			yield return new WaitForSeconds(Random.Range(0.6f, 1f));
			StartCoroutine(Firing());
		}
		else
		if (state == "Firing")
		{
			state = "Visible";
		//	sprite.SetActive(true);
			line.enabled = false;

			yield return new WaitForSeconds(Random.Range(0.6f, 1f));
			StartCoroutine(Invis());
		}
	}
	public override void DoDamage (int damage)
	{
		base.DoDamage(damage);
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
