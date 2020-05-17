using UnityEngine;
using System.Collections;

public class HealthBar_scr : MonoBehaviour {

	Player_scr player;
	Vector2 targetSize;

	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<Player_scr>();
	}
	int count = 0;
	void Update () {
		targetSize = new Vector2(Mathf.RoundToInt(((float)player.health / (float)player.maxHealth) * 64), 1);
		GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(GetComponent<RectTransform>().sizeDelta, targetSize, 0.2f);

		if (count < 0)
		{
			count = 10;
			GetComponent<RectTransform>().sizeDelta += new Vector2(2, 0);
		}
		count--;
	}

	public void Shake ()
	{
		GetComponent<RectTransform>().sizeDelta += new Vector2(0, 4);
	}
}
