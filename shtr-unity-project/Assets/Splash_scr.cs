using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash_scr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("GoToMenu", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GoToMenu ()
	{
		SceneManager.LoadScene("Menu");
	}	
}
