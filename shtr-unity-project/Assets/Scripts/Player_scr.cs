using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player_scr : MonoBehaviour {

	public float jumpStrength;
	public float mouseSensitivity = 1.0f;
    public float lerpSpeed = 0.5f;
    public int health;
    public int maxHealth;

	public GameObject gun;
	public GameObject knife;

	public AudioClip[] grunts = new AudioClip[1];

    GameObject cam;
    Effects_scr effects; 
    HealthBar_scr healthBar;
    CharacterController cc;
    Vector3 velocity;

  	float rotY = 0.0f;
 	float targetRotY;

 	Vector3 prevPos;
 	bool godMode = false;
	void Start () {

		health = 100;
		cam = GameObject.FindWithTag("MainCamera");
		cc = GetComponent<CharacterController>();
		healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar_scr>();
		effects = GameObject.Find("Effects").GetComponent<Effects_scr>();

		Cursor.lockState = CursorLockMode.Locked;
		
		Vector3 temp = transform.localRotation.eulerAngles;
		targetRotY = temp.y;
		rotY = targetRotY;

		prevPos = transform.position;
	}
	int stepCount;
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			godMode = !godMode;
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			SceneManager.LoadScene("Level"+(PowerUp_scr.level+1).ToString());
			PowerUp_scr.level+=1;
		}
		if (Input_scr.OnUIBackwardPressed())
		{
			Application.LoadLevel("Menu");
		}
		// Weapon
		if (Input_scr.OnKnifePressed() && knife.activeSelf == false)
		{
			StartCoroutine(Knife());
		}

		// Rotation
		targetRotY += Input_scr.GetCameraXMovement() * mouseSensitivity * Time.deltaTime;

		rotY = Mathf.LerpAngle(rotY, targetRotY, lerpSpeed);

		Quaternion localRotation = Quaternion.Euler(0, rotY, 0.0f);
        transform.localRotation = localRotation;

		// Movement
		cc.SimpleMove(transform.forward * Input_scr.GetPlayerMovement().y * 12);
		cc.SimpleMove(transform.right * Input_scr.GetPlayerMovement().x * 12);

		// Gravity
		cc.SimpleMove(Vector3.down);

		// Sound
		if (Vector3.Distance(transform.position, prevPos) > 0.5f)
		{
			if (stepCount < 0)
			{
				stepCount = 18;
				GetComponent<AudioSource>().Play();
			}
			prevPos = transform.position;
		}
		stepCount --;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.GetComponent<Gun_scr>())
		{
			Destroy(gun);
			gun = col.gameObject;
			gun.transform.parent = cam.transform;
			col.GetComponent<Gun_scr>().Equip();
		}
	}
	IEnumerator Knife ()
	{
		if (gun != null)
		{
			gun.SetActive(false);
		}

		knife.SetActive(true);
		knife.GetComponent<Knife_scr>().Knife();

		yield return new WaitForSeconds(0.2f);
		knife.SetActive(false);

		if (gun != null)
		{
			gun.SetActive(true);
		}
	}

	void OnApplicationFocus(bool focusStatus) {
  //      Cursor.lockState = CursorLockMode.Locked;
    }

    public void DoDamage (int damage)
    {
    	if (godMode == true)
    	{
    		return;
    	}
    	healthBar.Shake();
		effects.Flash(new Color(1, 0, 0, 0.7f), 0.02f);
    	health -= damage;
    	healthBar.Shake();

    	GetComponents<AudioSource>()[1].clip = grunts[Random.Range(0, grunts.Length)];
		GetComponents<AudioSource>()[1].Play();
    	if (health < 0)
    	{
    		SceneManager.LoadScene("GameOver");
    	}
    }
}
