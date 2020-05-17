using UnityEngine;
using System.Collections;

public class Enemy_scr : MonoBehaviour {

	public int health = 30;
	public GameObject bloodSplatterPrefab;
	public GameObject gorePrefab;
	public GameObject audioGOPrefab;

	protected GameObject player;
	protected GameObject cam;
	protected Effects_scr effects;

	protected Rigidbody rigid;
	protected void Start () {
		player = GameObject.FindWithTag("Player");
		cam = GameObject.FindWithTag("MainCamera");
		effects = GameObject.Find("Effects").GetComponent<Effects_scr>();

		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

	public virtual void DoDamage (int damage)
	{
		health -= damage;
		if (health < 0)
		{
			Destroy(gameObject);
		}
	}
	protected IEnumerator Flash(GameObject sr)
	{
		sr.GetComponent<SpriteRenderer>().color = Color.black;
		yield return new WaitForSeconds(0.1f);
		sr.GetComponent<SpriteRenderer>().color = Color.white;
	}
}
