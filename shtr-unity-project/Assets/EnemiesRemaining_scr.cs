using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemiesRemaining_scr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GetComponent<Text>().text = enemies.Length.ToString();

		if (enemies.Length == 0)
		{
			Invoke("LevelComplete", 1f);
		}
	}

	void LevelComplete ()
	{
		if (PowerUp_scr.level >= 4)
		{
			SceneManager.LoadScene("Credits");
		}
		else
		{
			SceneManager.LoadScene("PowerUp");
		}
	}
}
