using UnityEngine;
using System.Collections;

public class TimedDestroy_scr : MonoBehaviour {

	public float time;
	int count;
	void Start () {
		count = Mathf.RoundToInt(time / Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (count < 0)
		{
			Destroy(gameObject);
		}
		count--;
	}
}
