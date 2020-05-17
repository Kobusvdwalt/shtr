using UnityEngine;
using System.Collections;

public class Door_Scr : MonoBehaviour {

	GameObject door;
	Vector3 openPos;
	Vector3 closedPos;
	bool open = false;
	void Start () {
		door = transform.Find("DoorActual").gameObject;
		openPos = new Vector3(1, -4.75f, 0.3f);
		closedPos = new Vector3(1, 0, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		if (open == true)
		{
			door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, openPos, 0.2f);
		}
		else
		{
			door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, closedPos, 0.2f);
		}
	}

	void OnTriggerEnter (Collider  col)
	{
		if (col.tag == "Player" && open == false)
		{
			GetComponent<AudioSource>().Play();
			open = true;
		}
	}
	void OnTriggerExit (Collider  col)
	{
		if (col.tag == "Player")
		{
		//	open = false;
		}
	}
}
