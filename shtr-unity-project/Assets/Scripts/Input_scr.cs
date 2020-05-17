using UnityEngine;
using System.Collections;

public class Input_scr : MonoBehaviour {

	public static bool OnUIBackwardPressed ()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			return true;
		}
		return false;
	}
	public static float GetCameraXMovement ()
	{
		float delta;
		delta = Input.GetAxis("mouse x");

		return delta;
	}

	public static float GetCameraYMovement ()
	{
		float delta;
		delta = Input.GetAxis("mouse y");

		return delta;
	}

	public static Vector2 GetPlayerMovement ()
	{
		Vector2 movement = new Vector2(0, 0);
		if (Input.GetKey(KeyCode.A))
		{
			movement.x = -1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			movement.x = 1;
		}
		if (Input.GetKey(KeyCode.W))
		{
			movement.y = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			movement.y = -1;
		}

		return movement;
	}

	public static bool OnFire ()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			return true;
		}
		return false;
	}
	public static bool OnFirePressed ()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			return true;
		}
		return false;
	}
	public static bool OnKnife ()
	{
		if (Input.GetKey(KeyCode.Mouse1))
		{
			return true;
		}
		return false;
	}
	public static bool OnKnifePressed ()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			return true;
		}
		return false;
	}
}
