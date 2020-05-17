using UnityEngine;
using System.Collections;

public class Knife_scr : MonoBehaviour {

	Vector3 targetPos;
	Vector3 targetAngles;

	bool attacked = false;
	void Start () {
		targetPos = new Vector3(0.3f, -0.5f, 1.2f);
		targetAngles = new Vector3(0, -290, 90);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 0.3f);

	}
	public void Knife ()
	{
		attacked = false;
		GetComponent<AudioSource>().Play();
		transform.localPosition += new Vector3(0, 0, 1);
	}

	void OnTriggerStay (Collider col)
	{
		if (col.tag == "Enemy" && attacked == false)
		{
			col.GetComponent<Enemy_scr>().DoDamage(50);
			attacked = true;
		}
		if (col.tag == "Barrel" && attacked == false)
		{
			col.GetComponent<Barrel_scr>().DoDamage(50);
			attacked = true;
		}
	}

}
