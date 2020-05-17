using UnityEngine;
using System.Collections;

public class Pistol_scr : Gun_scr {

	
	new void Start () {
		base.Start();
		maxAmmo = 200;
		ammo = maxAmmo;
	}

	// Update is called once per frame
	new void Update () {
		base.Update();
		if (active == false)
		{
			return;
		}

		if (Input_scr.OnFirePressed()) 
		{
			ammo --;
			RaycastHit hit;

			if (Physics.Raycast(cam.transform.position + cam.transform.forward * 1, cam.transform.forward, out hit))
			{
				if (hit.transform.tag == "Solid")
				{
					GameObject a = Instantiate(hitSplashPrefab);
					a.transform.position = hit.point;
				}
				else
				if (hit.transform.tag == "Enemy")
				{
					hit.transform.GetComponent<Enemy_scr>().DoDamage(10);
				}

			}

			// Juice
			aud.Play();

			transform.localEulerAngles += new Vector3(0, 0, 20);
			transform.localPosition += new Vector3(0, 0, -0.2f);

			StartCoroutine(Flash());
			cam.GetComponent<Camera_scr>().Shake(0.3f);
			effects.Flash(Color.white * 0.4f, 0.4f);
		}
	}

	public override void Equip ()
	{
		base.Equip();
		targetPosition = new Vector3(0.3f, -0.3f, 1);
		targetAngles = new Vector3(0, -100, 0);

	}
}
