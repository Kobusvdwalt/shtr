using UnityEngine;
using System.Collections;

public class Gore_scr : MonoBehaviour {

	public GameObject bloodDecalPrefab;

	GameObject player;
	int count = 60;
	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		// Disable blood trail
		if (count < 0)
		{
			GetComponent<ParticleSystem>().Stop();
		}
		count --;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Solid")
		{
			GameObject a = Instantiate(bloodDecalPrefab);

			// Postion
			Vector3 collisionDelta = (col.contacts[0].point - transform.position).normalized;
			a.transform.position = col.contacts[0].point - collisionDelta * 0.1f;

			// Scale
			float randomScale = Random.Range(0.5f, 1f);
			a.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

			// Rotation
			RaycastHit hit;
			if (Physics.Raycast(transform.position, collisionDelta, out hit))
			{
				a.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			}

			// Color
			SpriteRenderer sr = a.transform.Find("Sprite").GetComponent<SpriteRenderer>();
			Color hsvTemp = Color.white;
			Color.RGBToHSV(sr.color, out hsvTemp.r, out hsvTemp.g, out hsvTemp.b);
			hsvTemp.g = Random.Range(0.9f, 1f);
			hsvTemp.b = Random.Range(0.4f, 0.7f);
			sr.color = Color.HSVToRGB(hsvTemp.r, hsvTemp.g, hsvTemp.b);
		}
	}
}
