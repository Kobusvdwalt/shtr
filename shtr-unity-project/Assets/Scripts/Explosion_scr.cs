using UnityEngine;
using System.Collections;

public class Explosion_scr : MonoBehaviour {

	SphereCollider col;
	void Start () {
		col = GetComponent<SphereCollider>();
	}

	void Update () {
		col.radius += 0.01f;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Enemy")
		{
			col.GetComponent<Enemy_scr>().DoDamage(60);
		}
	}
}
