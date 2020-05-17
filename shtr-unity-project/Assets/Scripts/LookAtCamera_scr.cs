using UnityEngine;
using System.Collections;

public class LookAtCamera_scr : MonoBehaviour {

	GameObject cam;
	void Start () {
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(cam.transform.position);
	}
}
