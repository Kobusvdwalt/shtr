using UnityEngine;
using System.Collections;

public class RocketLauncher_scr : Gun_scr {

	public GameObject rocketPrefab;
	public float reloadTime;

	int reloadCount;

	new void Start () {
		base.Start();
		maxAmmo = 20;
		ammo = maxAmmo;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		if (active == false)
		{
			return;
		}

		if (Input_scr.OnFirePressed() && reloadCount < 0) 
		{
			reloadCount = Mathf.RoundToInt(reloadTime / Time.deltaTime);
			ammo --;
			RaycastHit hit;

			GameObject a = Instantiate(rocketPrefab);
			a.transform.position = cam.transform.position + cam.transform.forward * 0.5f;
			a.transform.eulerAngles = cam.transform.eulerAngles;
			a.GetComponent<Rocket_scr>().speed = 30f;

			// Juice
			aud.Play();

			transform.localEulerAngles += new Vector3(0, 0, 20);
			transform.localPosition += new Vector3(0, 0, -0.2f);

			StartCoroutine(Flash());
			cam.GetComponent<Camera_scr>().Shake(0.4f);
			effects.Flash(Color.white * 0.4f, 0.4f);
		}

		if (reloadCount >= 10)
		{
			targetPosition = new Vector3(0.3f, -0.5f, 1);
			targetAngles = new Vector3(0, -100, -30);
		}
		else
		{
			targetPosition = new Vector3(0.3f, -0.3f, 1);
			targetAngles = new Vector3(0, -100, 0);
		}
		reloadCount --;
	}

	public override void Equip ()
	{
		base.Equip();
		targetPosition = new Vector3(0.3f, -0.3f, 1);
		targetAngles = new Vector3(0, -100, 0);

	}
}
