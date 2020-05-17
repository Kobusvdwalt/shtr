using UnityEngine;
using System.Collections;

public class Cursor_scr : MonoBehaviour {

	public float sensitivity = 0.5f;
	public Vector3 mousePos = Vector3.zero;

	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		mousePos.x += Input_scr.GetCameraXMovement() * sensitivity;
		mousePos.y += Input_scr.GetCameraYMovement() * sensitivity;

		mousePos.x = Mathf.Clamp(mousePos.x, -32, 31);
		mousePos.y = Mathf.Clamp(mousePos.y, -31, 32);

		transform.localPosition = mousePos;
	}
}
