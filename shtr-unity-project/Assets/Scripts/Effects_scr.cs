using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Effects_scr : MonoBehaviour {

	GameObject cam;

	List<GameObject> effectGOs = new List<GameObject>(10);
	List<Color> targetColors = new List<Color>(10);
	List<Color> beginColors = new List<Color>(10);
	List<float> lerpSpeeds = new List<float>(10);

	void Start () {
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	int count = 0;
	void Update () {
		for (int i=0; i < effectGOs.Count; i ++)
		{
//			if (effectGOs[i] == null)
//			{
//				effectGOs.RemoveAt(i);
//				targetColors.RemoveAt(i);
//				lerpSpeeds.RemoveAt(i);
//			}
//			else
			if (effectGOs[i].GetComponent<Image>().color == targetColors[i])
			{
				Destroy(effectGOs[i]);
				effectGOs.RemoveAt(i);
				targetColors.RemoveAt(i);
				lerpSpeeds.RemoveAt(i);
			}
			else
			{
				effectGOs[i].GetComponent<Image>().color = Color.Lerp(effectGOs[i].GetComponent<Image>().color, new Color(1, 1, 1, 0), lerpSpeeds[i]);
			}
		}
	}

	public void Flash(Color startColor, float speed)
	{
		GameObject a = new GameObject();
		a.AddComponent<Image>();
		a.transform.SetParent(transform, false);

		effectGOs.Add(a);

		a.GetComponent<Image>().color = startColor; 

		Color temp = startColor;
		temp.a = 0;
		targetColors.Add(temp);

		lerpSpeeds.Add(speed);
	}

}
