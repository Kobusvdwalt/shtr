using System.Collections;
using UnityEngine;

public class Camera_scr : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    public float clampAngle = 80.0f;
    public float lerpSpeed = 0.5f;

  	float rotX = 0.0f;
 	float targetRotX;

 	Vector3 targetPos;

    void Start ()
    {
        Vector3 temp = transform.localRotation.eulerAngles;
		targetRotX = temp.x;
		rotX = targetRotX;
		targetPos = transform.localPosition;
    }
 
    void Update () 
    {
		targetRotX += -Input_scr.GetCameraYMovement() * mouseSensitivity * Time.deltaTime;

		targetRotX = Mathf.Clamp(targetRotX, -clampAngle, clampAngle);
		rotX = Mathf.LerpAngle(rotX, targetRotX, lerpSpeed);

		Quaternion localRotation = Quaternion.Euler(rotX, 0, 0.0f);
        transform.localRotation = localRotation;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 0.3f);
    }

    public void Shake (float verocity)
    {
    	transform.position += -transform.forward * verocity;
		transform.position += transform.up * verocity;
	//	transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }
}