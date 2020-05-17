using UnityEngine;
using System.Collections;

public class MachineGun_scr : Gun_scr {

	public float fireRate;
	new void Start () {
		base.Start();
		maxAmmo = 50;
		ammo = maxAmmo;
	}

	int count = 0;
	new void Update () {
		base.Update();
		if (active == false)
		{
			return;
		}

		if (Input_scr.OnFire() && count < 0) 
		{
			count = Mathf.RoundToInt(fireRate / Time.deltaTime);
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
			cam.GetComponent<Camera_scr>().Shake(0.4f);
			effects.Flash(Color.white * 0.5f, 0.5f);
		}
		count --;
	}

	public override void Equip ()
	{
		base.Equip();
		targetPosition = new Vector3(0.3f, -0.4f, 1);
		targetAngles = new Vector3(0, -100, 0);

	}
}
