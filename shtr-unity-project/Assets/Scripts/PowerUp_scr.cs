using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerUp_scr : MonoBehaviour {

	public static int level = 1;
	public GameObject text;
	void Start () {
		Invoke("NextLevel", 2f);
		text.GetComponent<Text>().text = (level+1).ToString();
	}
	
	// Update is called once per frame
	void NextLevel () {
		
		level ++;
		SceneManager.LoadScene("Level" + level.ToString());
		
	}
}
