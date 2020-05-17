using UnityEngine;
using System.Collections;

public class Boss_scr : Enemy_scr {

	public GameObject turretBullet;
	public GameObject eyeBall;
	string state = "Turret"; // Turret, Lazerbeamer, EyeballSpawner
	void Start () {
		player = GameObject.FindWithTag("Player");
		StartCoroutine(Turret());
	}
	
	int turretCount = 0;
	void Update () {
		if (state == "Turret")
		{
			if (turretCount < 0)
			{
				for (int i=0 ; i < 3; i ++)
				{
					GameObject a = Instantiate(turretBullet);
					a.transform.position = transform.position;
					a.transform.LookAt(player.transform.position);
					a.transform.localEulerAngles += new Vector3(Random.Range(-2f, 2f), Random.Range(-20f, 20f), Random.Range(-2, 2f));
					a.GetComponent<Bullet_scr>().speed = 30;
					a.GetComponent<Bullet_scr>().damage = 10;
				}
				turretCount = 10;
			}
			turretCount --;
		}
		if(state == "Lazerbeamer")
		{
			
		}
		if(state == "EyeballSpawner")
		{
			
		}
	}

	IEnumerator Turret ()
	{
		state = "Turret";
		yield return new WaitForSeconds(6);
		StartCoroutine(LazerBeamer());
	}

	IEnumerator LazerBeamer ()
	{
		state = "LazerBeamer";
		yield return new WaitForSeconds(6);
		StartCoroutine(Turret());
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

//				GameObject c = Instantiate(audioGOPrefab);
//				c.transform.position = transform.position;
//				c.GetComponent<AudioSource>().clip = deathClip;
//				c.GetComponent<AudioSource>().pitch = Random.Range(0.7f, 0.9f);
//				c.GetComponent<AudioSource>().Play();
			}
		}
	}
}
