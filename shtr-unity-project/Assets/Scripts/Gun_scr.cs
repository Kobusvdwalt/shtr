using UnityEngine;
using System.Collections;

public class Gun_scr : MonoBehaviour {

	public GameObject hitSplashPrefab;

	public bool active = false;
	public int ammo;
	public int maxAmmo;

	protected GameObject cam;
	protected GameObject player;
	protected Effects_scr effects;

	protected Vector3 targetAngles;
	protected Vector3 targetPosition;
	protected GameObject flash;
	protected AudioSource aud;

	protected void Start () {
		cam = GameObject.FindWithTag("MainCamera");
		player = GameObject.FindWithTag("Player");
		effects = GameObject.Find("Effects").GetComponent<Effects_scr>();

		flash = transform.Find("Flash").gameObject;
		flash.SetActive(false);

		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	protected void Update () {
		
		if (active == false)
		{
			transform.localEulerAngles += new Vector3(0, 10, 0);
			return;
		}
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.2f);
		transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, targetAngles.x, 0.3f),
												 Mathf.LerpAngle(transform.localEulerAngles.y, targetAngles.y, 0.3f), 
												 Mathf.LerpAngle(transform.localEulerAngles.z, targetAngles.z, 0.3f));

		if (ammo < 0)
		{
			Destroy(gameObject);
		}
	}

	public virtual void Equip ()
	{
		active = true;
		Destroy(GetComponent<Collider>());
	}

	protected IEnumerator Flash ()
	{
		flash.SetActive(true);
		float temp = Random.Range(0.2f, 0.4f);
		flash.transform.localScale = new Vector3(temp, temp, 1);
		yield return new WaitForSeconds(0.05f);

		flash.SetActive(false);
	}
}
