using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_scr : MonoBehaviour {

	public GameObject cursor;
	public GameObject selectedBackground;
	public GameObject[] selections;

	int menuPos;
	GameObject selected;
	GameObject prevSelected;

	void Start () {
		selected = selections[0];
	}

	int count = 4;
	void Update () {
		// Audio
		if (selected != prevSelected)
		{
			if (selected != null)
			{
				GetComponent<AudioSource>().Play();
			}
			prevSelected = selected;

		}

		// Selection
		if (selected != null)
		{
			selected.GetComponent<Text>().color = Color.white;
		}
		selected = null;
		for (int i=0; i < selections.Length; i ++)
		{
			Rect properRect = selections[i].GetComponent<RectTransform>().rect;
			properRect.x += selections[i].GetComponent<RectTransform>().anchoredPosition.x;
			properRect.y += selections[i].GetComponent<RectTransform>().anchoredPosition.y;
			Vector2 cursorPos = cursor.transform.localPosition;

			if (cursorPos.x > properRect.x &&
				cursorPos.x < properRect.x + properRect.width &&
				cursorPos.y > properRect.y &&
				cursorPos.y < properRect.y + properRect.height)
			{
				selected = selections[i];
			}
		}

		if (selected == null)
		{
			selectedBackground.SetActive(false);
			return;
		}
		else
		{
			selectedBackground.SetActive(true);
		}

		selected.GetComponent<Text>().color = Color.cyan;
		selectedBackground.transform.localPosition = Vector3.Lerp(selectedBackground.transform.localPosition, selected.transform.localPosition, 0.3f);
		selectedBackground.GetComponent<RectTransform>().sizeDelta = 
			Vector2.Lerp(selectedBackground.GetComponent<RectTransform>().sizeDelta, selected.GetComponent<RectTransform>().sizeDelta, 0.3f);
		
		// Navigation
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (selected.name == "Play")
			{
				PowerUp_scr.level = 0;
				SceneManager.LoadScene("PowerUp");
			}
			if (selected.name == "Credits")
			{
				SceneManager.LoadScene("Credits");
			}
			if (selected.name == "Exit")
			{
				Application.Quit();
			}
		}

		// Juice
		if (count < 0)
		{
			count = 15;
			selectedBackground.GetComponent<RectTransform>().sizeDelta += new Vector2(4, 2);
		}
		count --;

	}

}
