using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoBar_scr : MonoBehaviour {

	GameObject player;
	GameObject gun;

	Vector2 targetSize;
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	int count;
	void Update () {
		if (player.GetComponent<Player_scr>().gun != null)
		{
			gun = player.GetComponent<Player_scr>().gun;
		}
		else
		{
			return;
		}

		int ammo = gun.GetComponent<Gun_scr>().ammo;
		int maxAmmo = gun.GetComponent<Gun_scr>().maxAmmo;

		targetSize = new Vector2(Mathf.RoundToInt(((float)ammo / (float)maxAmmo) * 64), 1);
		GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(GetComponent<RectTransform>().sizeDelta, targetSize, 0.2f);

		if (count < 0)
		{
			count = 15;
			GetComponent<RectTransform>().sizeDelta += new Vector2(2, 0);
		}
		count--;
	}

	public void Shake ()
	{
		GetComponent<RectTransform>().sizeDelta += new Vector2(0, 2);
	}
}
