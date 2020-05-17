using UnityEngine;
using System.Collections;

public class Bullet_scr : MonoBehaviour {

	public float speed;
	public int damage;
	Rigidbody rigid;
	void Start () {
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = transform.forward * speed;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Solid")
		{
			Destroy(gameObject);
		}
		if (col.tag == "Player")
		{
			col.GetComponent<Player_scr>().DoDamage(damage);
		}
	}
}
